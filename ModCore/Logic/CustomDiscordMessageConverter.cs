﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

namespace ModCore.Logic
{
	public class CustomDiscordMessageConverter : IArgumentConverter<DiscordMessage>
	{
		public async Task<Optional<DiscordMessage>> ConvertAsync(string value, CommandContext ctx)
		{
			if (value.StartsWith('^'))
			{
				var counttxt = value.Substring(1);

				if (!int.TryParse(counttxt, out int count))
					count = 1;

				if (count > 100)
					return Optional<DiscordMessage>.FromNoValue();

				var msgs = await ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id, count);
				return msgs.Last();
			}
			else
			{
				var conv = new DiscordMessageConverter();
				return await conv.ConvertAsync(value, ctx);
			}
		}
	}
}