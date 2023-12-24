using HashidsNet;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LivroDeReceitas.Api.Binder;

public class HashidsModelBinder : IModelBinder
{
    private readonly IHashids hashids;
    
    public HashidsModelBinder(IHashids hashids)
    {
        this.hashids = hashids ?? throw new ArgumentNullException(nameof(hashids));
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if(bindingContext is null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modeName = bindingContext.ModelName;

        var valueProviderResult = bindingContext.ValueProvider.GetValue(modeName);

        if(valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modeName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if(string.IsNullOrEmpty(value)) 
        {
            return Task.CompletedTask;
        }

        var ids = hashids.DecodeLong(value);

        if(ids.Length == 0)
        {
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(ids.First());

        return Task.CompletedTask;

    }

}
