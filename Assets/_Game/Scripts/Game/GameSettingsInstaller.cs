using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "RepeatBlock/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInstaller.Settings GameInstaller;

    [Space(10)]
    public BlockSettings Block;
    
    [Space(10)]
    public PlayerSettings Player;

    [Space(10)]
    public InitialActions.Settings InitialActions;

    [Space(10)]
    public RestartGame.Settings RestartGame;

    [Serializable]
    public class BlockSettings
    {
        public BlockTunables BlockTunables;
        public BlockMoveUpDown.Settings BlockMoveUpDown;
        public MoveTransformOnPlayerMove.Settings MoveTransformOnPlayerMove;
        public BlockOutOfViewChecker.Settings BlockOutOfViewChecker;
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerDeathHandler.Settings Death;
    }    

    public override void InstallBindings()
    {
        Container.BindInstance(GameInstaller).IfNotBound();

        Container.BindInstance(Block.BlockMoveUpDown).IfNotBound();
        Container.BindInstance(Block.MoveTransformOnPlayerMove).IfNotBound();
        Container.BindInstance(Block.BlockOutOfViewChecker).IfNotBound();        

        Container.BindInstance(Player.Death).IfNotBound();
        
        Container.BindInstance(InitialActions).IfNotBound();

        Container.BindInstance(RestartGame).IfNotBound();
    }
}
