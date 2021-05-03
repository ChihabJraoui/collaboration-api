using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Collaboration.ShareDocs.Application.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> IsPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder
            .MinimumLength(8).WithMessage("must be at least 8 characters")
            .MaximumLength(30).WithMessage("must be at most 30 characters")
            .Must(x => Regex.IsMatch(x, "[A-Z]")).WithMessage("Must contain at least one char UpperCase")
            .Must(x => Regex.IsMatch(x, "[a-z]")).WithMessage("Must contain at least one char LowerCase")
            .Must(x => Regex.IsMatch(x, "[0-9]")).WithMessage("Must contain at least one number")
            .Must(x => x.Any(c => !Char.IsLetterOrDigit(c) && !Char.IsWhiteSpace(c)))
                .WithMessage("Must contain at least one special char and not a whitespace");


        public static IRuleBuilderOptions<T, DateTime> IsDateTime<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
            => ruleBuilder.Must(x => x != DateTime.MinValue && x != DateTime.MaxValue)
                          .WithMessage("The value must be a valid DateTime.");

        public static IRuleBuilderOptions<T, Guid> IsGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
            => ruleBuilder.NotEqual(Guid.Empty)
                          .WithMessage("The value must be a valid Guid and must not be empty ( Ex: 14A1B4DB-8B23-4C23-B3B4-914D57853A49 ).");
        public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
           => ruleBuilder
            .NotEmpty()
            .Must(x => Guid.TryParse(x, out _))
                         .WithMessage("The value must be a valid Guid ( Ex: 14A1B4DB-8B23-4C23-B3B4-914D57853A49 ).");
        public static IRuleBuilderOptions<T, ICollection<Guid>> AreGuids<T>(this IRuleBuilder<T, ICollection<Guid>> ruleBuilder)
            => ruleBuilder.Must(x => !x.Contains(Guid.Empty))
                          .WithMessage("The value must be a list of valid Guids and must not be empty ( Ex: " +
                                       "[\"14A1B4DB-8B23-4C23-B3B4-914D57853A49\", \"41A14235-FFCA-4994-A8FA-AE3A87301DB6\"] ).");

        public static IRuleBuilderOptions<T, string> IsPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Must(x =>
                              x != null && Regex.IsMatch(x, @"\+?\(?\d{3}\)?-? *\d{3}-? *-?\d{4}"))
                          .WithMessage("The value must contain a phone number (Ex: +212-632-2452).");

        //public static IRuleBuilderOptions<T, string> IsJson<T, TObject>(this IRuleBuilder<T, string> ruleBuilder)
        //    => ruleBuilder.Must(
        //                      x =>
        //                      {
        //                          if (x == null)
        //                          {
        //                              return false;
        //                          }

        //                          var generator = new JSchemaGenerator();
        //                          var schema = generator.Generate(typeof(TObject));

        //                          return JObject.Parse(x).IsValid(schema);
        //                      })
        //                  .WithMessage("The value must be a valid Json.");

        //public static IRuleBuilderOptions<T, string> IsJson<T>(this IRuleBuilder<T, string> ruleBuilder)
        //    => ruleBuilder.Must(
        //                      x =>
        //                      {
        //                          if (x == null)
        //                          {
        //                              return false;
        //                          }

        //                          var isJson = true;
        //                          var value = x.Trim();

        //                          try
        //                          {
        //                              if (Regex.IsMatch(value, @"^\{\s*\}$"))
        //                              {
        //                                  return false;
        //                              }

        //                              JsonConvert.DeserializeObject<object>(value);
        //                          }
        //                          catch (Exception)
        //                          {
        //                              isJson = false;
        //                          }

        //                          return isJson;
        //                      })
        //                  .WithMessage("The value must be a valid Json and must not be empty.");

        public static IRuleBuilderOptions<T, string> IsAlphabetic<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Must(x =>
                              x != null && Regex.IsMatch(x, "[a-z]+", RegexOptions.IgnoreCase))
                          .WithMessage("The value must contain alphabets only.");

        public static IRuleBuilderOptions<T, string> IsSpacedAlphabetic<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Must(x =>
                              x != null && Regex.IsMatch(x, @"[a-z\s]+", RegexOptions.IgnoreCase))
                          .WithMessage("The value must contain alphabets with spaces only.");

        public static IRuleBuilderOptions<T, string> IsAlphaNumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Must(x =>
                              x != null && Regex.IsMatch(x, "[a-z0-9]+", RegexOptions.IgnoreCase))
                          .WithMessage("The value must contain alphanumerics only.");

        public static IRuleBuilderOptions<T, string> IsSpacedAlphaNumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Must(x =>
                              x != null && Regex.IsMatch(x, @"[a-z0-9\s]+", RegexOptions.IgnoreCase))
                          .WithMessage("The value must contain alphanumerics with spaces only.");
    }
}
