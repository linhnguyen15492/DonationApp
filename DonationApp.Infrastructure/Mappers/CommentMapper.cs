using DonationApp.Core.Entities;
using DonationApp.UseCase.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Mappers
{
    public static class CommentMapper
    {
        public static Comment ToComment(this CommentDto commentDto)
        {
            return new Comment
            {
                UserId = commentDto.UserId,
                Content = commentDto.Content,
                CampaignId = commentDto.CampaignId
            };
        }
    }
}