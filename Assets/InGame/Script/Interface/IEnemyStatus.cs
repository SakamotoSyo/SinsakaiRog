using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface IEnemyStatus
{
    public IStatusBase GetStatusBase();
    public IReactiveCollection<EnemyEffectData> GetEnemyTurnEffectOb();
    public void AttackDecision();
    public void AttackDecisionReset();
    public void StatusSet(EnemyStatusData enemy);
}
