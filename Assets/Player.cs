using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rb;
	[SerializeField] private Collider2D _collider;
	[SerializeField] private Actions _actions;
	private event UnityAction OnCollisionEnter2DAction;
	private StateMachine _stateMachine;

	private void Awake()
	{
		_actions = new Actions();
		_stateMachine = new StateMachine();

		// States
		var standbyState = new StandbyState(this);
		var movingState = new MovingState(this);
		var jumpingState = new JumpingState(this);
		var duckingState = new DuckingState(this);

		_stateMachine.AddState(standbyState);
		_stateMachine.AddState(movingState);
		_stateMachine.AddState(jumpingState);
		_stateMachine.AddState(duckingState);

		// Transitions
		_stateMachine.AddTwoWayTransition(standbyState, movingState, () => _actions.Player.Move.IsPressed());
		_stateMachine.AddTwoWayTransition(standbyState, duckingState, () => _actions.Player.Crouch.IsPressed());
		_stateMachine.AddTransition(standbyState, jumpingState, () => _actions.Player.Jump.IsPressed());
		_stateMachine.AddTransition(movingState, jumpingState, () => _actions.Player.Jump.IsPressed());
		OnCollisionEnter2DAction += () => _stateMachine.TriggerTransition(jumpingState, standbyState);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		OnCollisionEnter2DAction?.Invoke();
	}

	private void FixedUpdate()
	{
		_stateMachine.Update();
	}

	#region STATES
	public abstract class PlayerState : IState
	{
		public Player player;
		public PlayerState(Player player)
		{
			this.player = player;
		}

		public abstract void Update();
	}

	public class StandbyState : PlayerState
	{
		public StandbyState(Player player) : base(player)
		{
		}

		public override void Update()
		{ }
	}

	public class MovingState : PlayerState
	{
		public MovingState(Player player) : base(player)
		{ }

		public override void Update()
		{
			player._rb.velocityX = player._actions.Player.Move.ReadValue<Vector2>().x;
		}
	}

	public class JumpingState : PlayerState
	{
		public JumpingState(Player player) : base(player)
		{ }

		public override void Update()
		{
			player._rb.velocityX = player._actions.Player.Move.ReadValue<Vector2>().x;
		}
	}

	public class DuckingState : PlayerState
	{
		public DuckingState(Player player) : base(player)
		{ }

		public override void Update()
		{
			player._rb.velocityX = player._actions.Player.Move.ReadValue<Vector2>().x * 0.5f;
		}
	}
	#endregion
}
