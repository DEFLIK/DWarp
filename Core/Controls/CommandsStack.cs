using System.Collections.Generic;

namespace DWarp.Core.Controls
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class CommandsStack<TItem>
    {
        public readonly Stack<ICommand> Stack;
        public readonly Stack<ICommand> Canceled;
        public readonly int Limit;

        public CommandsStack(int limit)
        {
            Stack = new Stack<ICommand>();
            Canceled = new Stack<ICommand>();
            Limit = limit;
        }

        public void AddCommand(ICommand command)
        {
            if (Stack.Count < Limit)
            {
                Stack.Push(command);
                command.Execute();
                Canceled.Clear();
            }
        }

        public void RollBack()
        {
            if (Stack.Count > 0)
            {
                var command = Stack.Pop();
                command.Undo();
                Canceled.Push(command);
            }
        }

        public void RollForward()
        {
            if (Canceled.Count > 0)
            {
                var command = Canceled.Pop();
                command.Execute();
                Stack.Push(command);
            }
        }

        public void ResetBack()
        {
            while (Canceled.Count > 0)
                Stack.Push(Canceled.Pop());
        }
    }
}
