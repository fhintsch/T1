using System;
namespace T1
{
	public interface ITabView
	{
        public Task ActivateAsync(bool foreground);
    }
}

