using UnityEngine;

[CreateAssetMenu(fileName = "NewJokerChoose", menuName = "PawnObject/New joker choose pawn", order = 3)]
public class JokerChoosePawn : PawnObjectSpecial
{
    public override bool OnDropWithPawn(Pawn owner, Cell cellToInteract)
    {
        return false;
    }

    public override bool OnDropWithNoPawn(Pawn owner, Cell cellToInteract)
    {
/*        UIManager.Instance.ChooseNewPawnPopup.UpdateData(owner, cellToInteract);
        UIManager.Instance.AddPopup(UIManager.Instance.ChooseNewPawnPopup);*/

        return true;
    }
}