using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRichPresence.Activities
{
    interface ActivityHolder
    {
        Activity GetActivity();
    }
}
