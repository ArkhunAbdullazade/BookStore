using BookStore.Models.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Messages.Base;
public class SendLoginedUserMessage : IMessage
{
    public User LoginedUser { get; set; }
    public SendLoginedUserMessage(User loginedUser)
    {
        this.LoginedUser = loginedUser;
    }
}
