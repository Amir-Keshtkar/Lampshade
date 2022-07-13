namespace _01_LampshadeQuery.Contract.Inventory {
    public interface IInventoryQuery {
        StockStatus CheckStockStatus(IsInStock command);
    }
}