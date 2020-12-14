using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle.BookStore.Models;

namespace WebGentle.BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private NewBookAlertConfig _newBookAlertconfiguration;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertconfiguration)
        {
            _newBookAlertconfiguration = newBookAlertconfiguration.CurrentValue;
            newBookAlertconfiguration.OnChange(config => 
            {
                _newBookAlertconfiguration = config;
            });
        }

        public string GetName()
        {
            return _newBookAlertconfiguration.BookName;
        }
    }
}
