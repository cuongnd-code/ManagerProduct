using System.ComponentModel;
using ManagerProduct.Model;

namespace ManagerProduct.ViewModel
{
	public class ProductSelectable : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public double Price { get; set; }
		public required int productID { get; set; }
		public required Product product { get; set; }

		private bool _isSelected;
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (_isSelected != value)
				{
					_isSelected = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
				}
			}
		}

		private int _quantity;
		public int Quantity
		{
			get => _quantity;
			set
			{
				if (_quantity != value)
				{
					_quantity = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
				}
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
