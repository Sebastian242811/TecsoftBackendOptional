using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Social.Domain.Repositories;
using VirtualExpress.Social.Services;

namespace VirtualExpress.Api.Test.Social.Test
{
    public class UnitTestCommentary
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoCommentaryReturnEmptyCollection()
        {
            //Arrange
            var mockCommentaryRepository = GetDefaultICommentaryRepositoryInstance();
            mockCommentaryRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Commentary>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkRepositoryInstance();
            var service = new CommentaryService(
                mockCommentaryRepository.Object,
                mockUnitOfWork.Object
                );
            //Act
            List<Commentary> commentaries = (List<Commentary>)await service.ListAsync();
            var commentaryCount = commentaries.Count;
            //Assert
            commentaryCount.Should().Equals(0);
        }


        private Mock<ICommentaryRepository> GetDefaultICommentaryRepositoryInstance()
        {
            return new Mock<ICommentaryRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
