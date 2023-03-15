using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chat.WebAPI.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {
            return modelState.Values.First().Errors.First().ErrorMessage;
        }
    }
}
