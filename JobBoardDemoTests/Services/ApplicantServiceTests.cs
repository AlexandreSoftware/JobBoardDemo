
// using System.Linq;
// using AutoMapper;
// using Bogus;
// using FluentAssertions;
// using JobBoardRepository.Domain;
// using JobBoardRepository.Interface;
// using JobBoardServices.Interface;
// using JobBoardServices.Profile;
// using JobBoardServices.Service;
// using Moq;
// using Xunit;
//
// namespace JobBoardDemoTests.Services;
//
// public class ApplicantServiceTests
// {
//     private IApplicantService _js;
//     public Mock<IApplicantRepository> _jr;
//     public Mock<IJobApplicantRepository> _jar;
//     public Faker<ApplicantDTO> faker;
//     public Mapper m ;
//     public ApplicantServiceTests()
//     {
//         
//         ApplicantProfile myProfile = new ApplicantProfile();
//         var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
//         m = new Mapper(configuration);
//         this._jr = new Mock<IApplicantRepository>();
//         this._jar = new Mock<IJobApplicantRepository>();
//         this._js = new ApplicantService(_jr.Object, m,_jar.Object);
//         faker = new Faker<ApplicantDTO>()
//             .RuleFor<int>(property:x=>x.Id,setter:y=>y.IndexFaker)
//             .RuleFor<string>(property:x=>x.Name,setter:y=>y.Name.FullName())
//             .RuleFor<double>(property:x=>x.WageExpectation,setter:y=>y.Finance.Random.Double());
//     }
//
//     [Fact]
//     public async void OnGet_ValidValue_ShouldReturnMappedObject()
//     {
//         //Arrange
//         var data = faker.Generate(20).ToArray();
//         _jr.Setup(x => x.Get()).ReturnsAsync(data);
//         var mockresult = data.Select(x =>
//         {
//             return new Applicant()
//             {
//                 id = x.ProductId,
//                 description = x.Description,
//                 title = x.Title,
//                 maxPay = x.MaxPay,
//                 minPay = x.MinPay,
//                 subTitle = x.SubTitle
//             };
//         }).ToArray();
//         //Act   
//         var result = await _js.Get();
//         //Assert
//         result.Should().BeEquivalentTo(mockresult);
//     }
//     [Fact]
//     public async void OnPost_FalseValue_ShouldReturnFalse()
//     {
//         //Arrange
//         _jr.Setup(x => x.Post(null)).ReturnsAsync(false);
//         //Act   
//         var result = _js.Post(null);
//         //Assert
//         Assert.False( await result);
//     }
//
//     [Fact]
//     public async void OnPost_ValidValue_ShouldReturnMappedObject()
//     {
//         ;
//         //Arrange
//         var data = faker.Generate(1)[0];
//         _jr.Setup(x => x.Post(It.IsAny<ApplicantDTO>())).ReturnsAsync(true);
//         var mockresult = new Applicant()
//         {
//             id = data.ProductId,
//             description = data.Description,
//             title = data.Title,
//             maxPay = data.MaxPay,
//             minPay = data.MinPay,
//             subTitle = data.SubTitle
//         };
//         //Act   
//         var result = _js.Post(mockresult);
//         //Assert
//         Assert.True(await result);
//     }
//
//     [Fact]
//     public async void OnGetId_ValidId_ShouldReturnMappedObject()
//     {
//         //Arrange
//         var data = faker.Generate(1)[0];
//         _jr.Setup(x => x.GetId(1)).ReturnsAsync(data);
//         var mockresult = new Applicant()
//         {
//             id = data.ProductId,
//             description = data.Description,
//             title = data.Title,
//             maxPay = data.MaxPay,
//             minPay = data.MinPay,
//             subTitle = data.SubTitle
//         };
//         //Act   
//         var result = await _js.GetId(1);
//         //Assert
//         result.Should().BeEquivalentTo(mockresult);
//     }
//     [Fact]
//     public async void OnDelete_FalseValue_ShouldReturnFalse()
//     {
//         //Arrange
//         _jr.Setup(x => x.Delete(1)).ReturnsAsync(false);
//         //Act   
//         var result = _js.Delete(1);
//         //Assert
//         Assert.False(await result);
//     }
//
//     [Fact]
//     public async void OnDelete_ValidValue_ShouldReturnTrue()
//     {
//         //Arrange
//         var data = faker.Generate(1)[0];
//         _jr.Setup(x => x.Delete(1)).ReturnsAsync(true);
//         //Act   
//         var result = _js.Delete(1);
//         //Assert
//         Assert.True(await result);
//     }
//     [Fact]
//     public async void OnPut_FalseValue_ShouldReturnFalse()
//     {
//         //Arrange
//         _jr.Setup(x => x.Put(It.IsAny<ApplicantDTO>())).ReturnsAsync(false);
//         //Act   
//         var result = _js.Put(new Applicant());
//         //Assert
//         Assert.False(await result);
//     }
//
//     [Fact]
//     public async void OnPut_ValidValue_ShouldReturnMappedObject()
//     {
//         //Arrange
//         var data = faker.Generate(1)[0];
//         _jr.Setup(x => x.Put(It.IsAny<ApplicantDTO>())).ReturnsAsync(true);
//         var mockresult = new Applicant()
//         {
//             id = data.ProductId,
//             description = data.Description,
//             title = data.Title,
//             maxPay = data.MaxPay,
//             minPay = data.MinPay,
//             subTitle = data.SubTitle
//         };
//         //Act   
//         var result = _js.Put(mockresult);
//         //Assert
//         Assert.True(await result);
//     }
// }