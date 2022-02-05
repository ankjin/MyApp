using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public class IJSInteropExtension : IDisposable
    {
        private readonly IJSRuntime _js;
        //private DotNetObjectReference<HelloHelper> objRef;

        public IJSInteropExtension(IJSRuntime js)
        {
            _js = js;
        }

        public async ValueTask<string> CallHelloHelperSayHello(string name)
        {
            //objRef = DotNetObjectReference.Create(new HelloHelper(name));

            //return js.InvokeAsync<string>(
            //    "exampleJsFunctions.sayHello",
            //    objRef);

            return await _js.InvokeAsync<string>("");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
