﻿using Discord;
using Discord.Commands;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Modix.Modules
{
    /// <summary>
    /// Used to test feature work on a private server. The contents of this module can be changed any time.
    /// </summary>
    [Group("debug"), RequireUserPermission(GuildPermission.BanMembers)]
    public class DebugModule : ModuleBase
    {
        [Command("throw")]
        public Task Throw([Remainder]string text)
        {
            Log.Error("Error event");
            Log.Error(new Exception("ExceptionEvent"), "ExceptionEvent Template");
            Log.Information("Extra stuff we shouldn't see");

            return Task.CompletedTask;
        }

        [Command("leave")]
        public async Task Leave(ulong guildId)
        {
            var guild = await Context.Client.GetGuildAsync(guildId);

            if(guild == null)
            {
                await ReplyAsync($"Modix is not joined to a guild with id {guildId}");
                return;
            }

            await guild.LeaveAsync();
            await ReplyAsync($"Left a guild named {guild.Name}");
        }

        [Command("joined")]
        public async Task Joined()
        {
            var guilds = await Context.Client.GetGuildsAsync();

            var output = string.Join(", ", guilds.Select(a => $"{a.Id}: {a.Name}"));
            await ReplyAsync(output);
        }
    }
}
