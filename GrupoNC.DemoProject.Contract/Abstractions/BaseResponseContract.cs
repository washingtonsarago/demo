namespace GrupoNC.DemoProject.Abstractions
{
    using System.Collections.Generic;

    public abstract class BaseResponseContract<C> where C : BaseResponseContract<C>
    {
        private IList<string>? _MessageList = new List<string>();
        public IList<string>? MessageList 
        { 
            get => _MessageList; 
            set => _MessageList = value;
        }
    }
}