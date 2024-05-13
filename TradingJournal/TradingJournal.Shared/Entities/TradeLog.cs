using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TradingJournal.Shared.Entities
{
    public class TradeLog
    {
        [Key]
        public int TradeNumber { get; set; }

        [Display(Name = "StartTime")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime StartTime { get; set; }

        [Display(Name = "EndTime")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Asset")]
        [MaxLength(20, ErrorMessage = "More than 20 charachters are not allowed")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Asset { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Risk")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public double Risk { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Pnl")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public double Pnl { get; set; }

        public double WinRate { get; set; }

        public double RiskRewardRatio { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Comission")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public double Comission { get; set; }

        public double NetPnl { get; set; }

        [ForeignKey("AccountsAccNumber")]
        [JsonIgnore]
        public Account Accounts { get; set; }
        public int AccountsAccNumber { get; set; }
    }
}
