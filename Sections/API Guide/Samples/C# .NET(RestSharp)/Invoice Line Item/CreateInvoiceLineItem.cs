/// <summary>
/// API Request to create invoice line items on ePay
/// </summary>
/// <param name="json">JSON String</param>
/// <param name="URL">URL of the ePay site</param>
/// <param name="invoiceEntryInfo">Invoice object associated to the line items</param>
/// <param name="epayTokenList">ePay token object</param>
/// <param name="responses">Returned response object</param>
public void CreateInvoiceLineItem(string json, string URL, InvoiceLineItemRequest invoiceEntryInfo, EPayToken epayTokenList, ref CreateResponse responses)
{
	// Sample request and response
	// ------------------------------------------------------
	// Go to https://github.com/NodusTechnologies/ePay-Advantage/blob/master/Sections/API%20Guide/Sections/APIs/Invoice%20Line%20Items.md for more details about request and response.
	// Go to https://github.com/NodusTechnologies/ePay-Advantage/blob/master/Sections/API%20Guide/Sections/Objects/Invoice%20Line%20Item.md for more details about the object.
	// ------------------------------------------------------
	
	var client = new RestClient(CheckURL(URL) + "API/invoiceLineItem?" + CreateGetRequestURL(invoiceEntryInfo));
	var request = new RestRequest(Method.POST);
	request.AddHeader("content-type", "application/json");
	request.AddHeader("authorization", "Bearer " + epayTokenList.access_token);
	request.AddParameter("application/json", json, ParameterType.RequestBody);
	IRestResponse response = client.Execute(request);

	if (response.StatusCode == System.Net.HttpStatusCode.OK)
	{
		try
		{
			JsonDeserializer deserial = new JsonDeserializer();
			responses = deserial.Deserialize<CreateResponse>(response);
		}
		catch
		{
			responses.Message = "Token validation failed";
			responses.Result = "false";
		}
	}
	else
	{
		responses.Message = "Bad HTTP Request";
		responses.Result = "false";
	}
}