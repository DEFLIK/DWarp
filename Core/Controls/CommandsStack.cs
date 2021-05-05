using System.Collections.Generic;

namespace DWarp.Core.Controls
{
    public interface ICommand
    {
        bool Execute();
        bool Undo();
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
                var command = Stack.Peek();
                if(command.Undo())
                    Canceled.Push(Stack.Pop());
            }
        }

        public void RollForward()
        {
            if (Canceled.Count > 0)
            {
                var command = Canceled.Peek();
                if(command.Execute())
                    Stack.Push(Canceled.Pop());
            }
        }

        public void ResetBack()
        {
            while (Canceled.Count > 0)
                Stack.Push(Canceled.Pop());
        }
    }
}
