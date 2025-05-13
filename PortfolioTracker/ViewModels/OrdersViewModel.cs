using System.Collections.ObjectModel;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels
{
    class OrdersViewModel
    {
        public RelayCommand DeleteOrderCommand { get; set; }
        private readonly PortfolioManager _manager;
        private Order _selectedOrder;
        public Order SelectedOrder { get => _selectedOrder; set { _selectedOrder = value; DeleteOrderCommand.RaiseCanExecuteChanged(); } }
        public ObservableCollection<Order> Orders { get; set; }
        public OrdersViewModel(PortfolioManager manager)
        {
            _manager = manager;
            Orders = new(_manager.PortfolioDatabase.Orders.OrderBy(o => o.Ticker));
            DeleteOrderCommand = new RelayCommand(DeleteOrder, CanDeleteOrder);
        }
        private async void DeleteOrder(object? obj)
        {
            try
            {
                var orderId = await _manager.GetOrder(_selectedOrder.Id);
                if (orderId != null)
                {
                    await _manager.RemoveOrder(orderId);
                    Orders.Remove(orderId);
                }
            }
            catch { }
        }
        private bool CanDeleteOrder(object? obj) => _selectedOrder != null;
    }
}
