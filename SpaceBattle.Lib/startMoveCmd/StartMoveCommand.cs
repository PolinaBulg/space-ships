namespace SpaceBattle.Lib;


    public class StartMoveCommand: ICommand
    {
        private IMoveCommandStartable moveCommandStartable;
        public StartMoveCommand(IMoveCommandStartable moveCommandStartable)
        {
            this.moveCommandStartable = moveCommandStartable;
        }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}

