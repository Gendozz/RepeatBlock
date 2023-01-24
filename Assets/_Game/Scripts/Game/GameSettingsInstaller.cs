using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "RepeatBlock/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInstaller.Settings GameInstaller;

    [Space(10)]
    public BlocksSpawner.Settings BlocksSpawner;

    [Space(10)]
    public PathGenerator.Settings BlockPathGenerator;

    [Space(10)]
    public BlockSettings Block;

    [Space(10)]
    public AllBlocksMovedChecker.Settings AllBlocksMovedChecker;
    
    [Space(10)]
    public PlayerSettings Player;

    [Space(10)]
    public OpponentSettings Opponent;

    [Space(10)]
    public RotateInDirection.Settings RotateInDirection;

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
    
    [Serializable]
    public class OpponentSettings
    {
        public OppenentMoveHandler.Settings PathRepeater;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(GameInstaller).IfNotBound();

        Container.BindInstance(BlocksSpawner).IfNotBound();
        Container.BindInstance(BlockPathGenerator).IfNotBound();
        Container.BindInstance(Block.BlockMoveUpDown).IfNotBound();
        Container.BindInstance(Block.MoveTransformOnPlayerMove).IfNotBound();
        Container.BindInstance(Block.BlockOutOfViewChecker).IfNotBound();
        Container.BindInstance(AllBlocksMovedChecker).IfNotBound();
        

        Container.BindInstance(RotateInDirection).IfNotBound();
        Container.BindInstance(Player.Death).IfNotBound();
        
        Container.BindInstance(Opponent.PathRepeater).IfNotBound();

        Container.BindInstance(InitialActions).IfNotBound();

        Container.BindInstance(RestartGame).IfNotBound();
    }

    private void OnValidate()
    {
        Block.MoveTransformOnPlayerMove.moveDuration = RotateInDirection.moveDuration;
        Opponent.PathRepeater.oneMoveDuration = RotateInDirection.moveDuration;

    }
}
