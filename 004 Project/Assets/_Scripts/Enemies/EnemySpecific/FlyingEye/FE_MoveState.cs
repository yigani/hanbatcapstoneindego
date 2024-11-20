using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FE_MoveState : MoveState
{
    private FlyingEye enemy;
    public FE_MoveState(Entity entity, MonsterStateMachine stateMachine, string animBoolName, D_MoveState stateData, FlyingEye enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (enemy.lastKnownPlayerPosition != null)
        {
            float direction = Mathf.Sign(enemy.lastKnownPlayerPosition.Value.x - enemy.transform.position.x);
            Movement?.SetVelocityX(stateData.movementSpeed * direction * enemyStats.MoveSpeed);

            // ������ ��ġ�� �������� ���
            if (enemy.IsAtLastKnownPlayerPosition())
            {
                enemy.ClearLastKnownPlayerPosition();
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
