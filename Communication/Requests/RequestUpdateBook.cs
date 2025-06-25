using LibraryApi.Types;

namespace LibraryApi.Communication.Requests;

public class RequestUpdateBook
{
  public required string Title  { get; set; }
  public required string Author  { get; set; }
  public required IGenre Genre  { get; set; }
  public required double Price  { get; set; }
  public required int StorageQuantity  { get; set; }
}