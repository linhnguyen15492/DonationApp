using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Core.Shared;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Result<int>> AddCommentAsync(IModel model)
        {
            var data = model as CommentModel;

            var comment = new Comment
            {
                CampaignId = data!.CampaignId,
                Content = data.Content,
                UserId = data.UserId
            };

            try
            {
                await _commentRepository.InsertAsync(comment);
                await _commentRepository.SaveAsync();

                return Result<int>.Success(comment.Id);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);

            }
        }

        public Task<Result<int>> DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IDto>> GetCommentsByCampaignIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> UpdateCommentAsync(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
