using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FrontTargetStrategy : ITargetStrategy {
    public List<UnitStats> SelectTargets(UnitStats[] units) {
        var target = units.FirstOrDefault(unit => unit != null && !unit.isDead());
        return target != null ? new List<UnitStats> { target } : new List<UnitStats>();
    }
}
