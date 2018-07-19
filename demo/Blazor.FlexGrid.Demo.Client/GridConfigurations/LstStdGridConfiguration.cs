using Blazor.FlexGrid.Components.Configuration;
using Blazor.FlexGrid.Components.Configuration.Builders;
using Blazor.FlexGrid.Demo.Shared;

namespace Blazor.FlexGrid.Demo.Client.GridConfigurations
{
    public class LstStdGridConfiguration : IEntityTypeConfiguration<LstStd>
    {
        public void Configure(EntityTypeBuilder<LstStd> builder)
        {
            builder.Property(e => e.odt)
                .HasCaption("Order date")
                .HasValueFormatter(d => d.ToShortDateString());

            builder.Property(e => e.fio)
                .HasCaption("Client Name")
                .HasOrder(1)
                .HasValueFormatter(s => $"{s}");

            builder.Property(e => e.csum)
                .HasCaption("Order Sum")
                .IsSortable();
        }
    }
}
