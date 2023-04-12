using OrdersManagement.Domain.Core.Events;
using OrdersManagement.Domain.CQRS.Events;
using System.Text.Json;

namespace OrderManagementApplication
{
    public class OrderHistory
    {
        public static IList<OrderHistoryData> HistoryData { get; set; }

        public static IList<OrderHistoryData> ToJavaScriptHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<OrderHistoryData>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<OrderHistoryData>();
            var last = new OrderHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new OrderHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    ProductName = string.IsNullOrWhiteSpace(change.ProductName) || change.ProductName == last.ProductName
                        ? ""
                        : change.ProductName,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void HistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var @event in storedEvents)
            {
                var data = new OrderHistoryData();
                var values = JsonSerializer.Deserialize<Dictionary<string, string>>(@event.Data)!;

                switch (@event.MessageType)
                {
                    case nameof(OrderCreated):
                        data.ProductName = values["ProductName"];
                        data.Action = nameof(OrderCreated);
                        data.When = values["Timestamp"];
                        data.Id = values["Id"];
                        break;
                    case nameof(OrderUpdated):
                        data.ProductName = values["ProductName"];
                        data.Action = nameof(OrderUpdated);
                        data.When = values["Timestamp"];
                        data.Id = values["Id"];
                        break;
                    case nameof(OrderCanceled):
                        data.Action = nameof(OrderCanceled);
                        data.When = values["Timestamp"];
                        data.Id = values["Id"];
                        break;
                }
                HistoryData.Add(data);
            }
        }
    }
}
