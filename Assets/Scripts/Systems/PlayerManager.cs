using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems.Input;
using AgeOfKing.Utils;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public enum SIDE { LEFT, RIGHT }

    public interface IPlayer
    {
        public string Name { get; }
        public SIDE PlayGroundSide{get;}
        public Village GetVillage{ get; }
        public KingdomPreset GetKingdomPreset { get; }
        public ICommandController CommandController { get; }
        public IBuildingTileChecker BuildingTileChecker { get; }

        public void SwapPlayer(bool isOn); 
        public void Release();

    }

    public class Player : IPlayer
    {
        public Player(KingdomPreset kingdomPreset, SIDE playground,string playerName)
        {
            Name = playerName;
            PlayGroundSide = playground;
            GetKingdomPreset = kingdomPreset;
            GetVillage = new Village(this, kingdomPreset);
            _inputManager = new PCInputManager();
            _mapInput = new PlayerMapInput(_inputManager, this);
            BuildingTileChecker = new PlayerBuildingTileChecker(_inputManager, _mapInput, this, playground);
            CommandController = new PlayerCommandController(this);
            _playerMapHighlighter = new PlayerMapHighlighter();

            _mapInput.OnCellHover += CommandController.CalculatePath;
            _mapInput.OnCellSelected += CommandController.SetControllableUnit;
            _mapInput.OnCellUnitCommand += CommandController.UnitCommand;

            CommandController.OnPathCreated += _playerMapHighlighter.ShowPath;
            CommandController.OnCommandStateReset += _playerMapHighlighter.Close;

            BuildingTileChecker.OnCheckingTiles += _playerMapHighlighter.ShowTile;
            BuildingTileChecker.OnBuildCommand += CommandController.BuildCommand;
            BuildingTileChecker.OnPlacementEnd += _playerMapHighlighter.Close;
        }


        public string Name { get; private set; }
        public SIDE PlayGroundSide { get ; private set; }

        public Village GetVillage { get; private set; }

        public KingdomPreset GetKingdomPreset { get; private set; }

        public ICommandController CommandController { get; private set; }

        public IBuildingTileChecker BuildingTileChecker { get; private set; }

        IInputManager _inputManager;
        IMapInput _mapInput;
        IHighlighter _playerMapHighlighter;

        public void SwapPlayer(bool isOn)
        {
            _inputManager.SwapInputState(isOn);
        }

        public void Release()
        {
            GetVillage.UnbindUI_PlayerVillage();

            _mapInput.UnbindInput(_inputManager);
            _mapInput.OnCellHover -= CommandController.CalculatePath;
            _mapInput.OnCellSelected -= CommandController.SetControllableUnit;
            _mapInput.OnCellUnitCommand -= CommandController.UnitCommand;

            CommandController.OnPathCreated -= _playerMapHighlighter.ShowPath;
            CommandController.OnCommandStateReset -= _playerMapHighlighter.Close;

            BuildingTileChecker.OnCheckingTiles -= _playerMapHighlighter.ShowTile;
            BuildingTileChecker.OnBuildCommand -= CommandController.BuildCommand;
            BuildingTileChecker.OnPlacementEnd -= _playerMapHighlighter.Close;

            GetVillage = null;
            CommandController = null;
            BuildingTileChecker = null;
            _inputManager = null;
            _mapInput = null;
            _playerMapHighlighter = null;
        }
    }


    public static class PlayerManager 
    {
        static IPlayer p1;
        static IPlayer p2;

        public static IPlayer P1 { get => p1; }
        public static IPlayer P2 { get => p2; }

        public static void CreatePlayer(KingdomPreset kingdomPreset_P1,KingdomPreset kingdomPreset_P2)
        {
            p1 = new Player(kingdomPreset_P1,SIDE.LEFT,"P1");
            p2 = new Player(kingdomPreset_P2,SIDE.RIGHT,"P2");

            p1.GetVillage.BindUI_PlayerVillage();
            p1.SwapPlayer(true);
            TurnManager.GetInstance.StartGame();

            SpawnKings(p1);
            SpawnKings(p2);
        }

        /// <summary>
        /// Swap player and controller
        /// </summary>
        /// <param name="player"></param>
        public static void SwapPlayer(IPlayer player)
        {
            p1.CommandController.Release();
            p2.CommandController.Release();

            if (player == p1)
            {
                p1.SwapPlayer(true);
                p2.SwapPlayer(false);
            }
            else
            {
                p2.SwapPlayer(true);
                p1.SwapPlayer(false);
            }

        }

        public static void OnGameOver()
        {
            p1.Release();
            p2.Release();
        }

        static void SpawnKings(IPlayer player)
        {
            Abstract.Components.AUnit unit = UnitFactory.GetInstance.Produce(player.GetKingdomPreset.GetKigdomKing, player);
            Vector3Int cellLocation = MapUtils.GetKingStartLocation(player.PlayGroundSide);
            unit.Draw(cellLocation);
            unit.MoveTo(cellLocation);
        }

    }

}