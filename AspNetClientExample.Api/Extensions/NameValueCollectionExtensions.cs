using System.Collections.Specialized;

namespace AspNetClientExample.Api.Extensions;

public static class NameValueCollectionExtensions
{
	public static NameValueCollection AddValueIfNotNull<TValue>(
		this NameValueCollection collection,
		string name,
		TValue? value)
	{
		if (value is not null)
			collection.Add(name, value.ToString());

		return collection;
	}

	public static NameValueCollection AddValuesIfNotNull<TValue>(
		this NameValueCollection collection,
		string name,
		TValue?[]? values)
	{
		if (values?.Any() == true)
			foreach (var value in values)
				collection.AddValueIfNotNull(name, value);

		return collection;
	}
}