using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Moq;
using obligDiagnoseVerktøyy.Repository.interfaces;
using obligDiagnoseVerktøyy.Model.entities;
using obligDiagnoseVerktøyy.Repository.implementation;
using KundeAppTest;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using obligDiagnoseVerktøyy.Controllers.implementations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using obligDiagnoseVerktøyy.Model.DTO;
using obligDiagnoseVerktøyy.Model.viewModels;
using System.Threading.Tasks;
using System.Text;

namespace TestProjectAngular
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IDiagnoseGruppeRepository> _diagnoseGruppeRepository;
        private Mock<IDiagnoseRepository> _diagnoseRepository;
        private Mock<ISymptomGruppeRepository> _symptomGruppeRepository;
        private Mock<ISymptomBildeRepository> _symptomBildeRepository;
        private Mock<ISymptomRepository> _symptomRepository;
        private const string _loggetInn = "InnLogget";
        private const string _ikkeLoggetInn = "";

        private  Mock<ILogger<DiagnoseController>> _mockLog;

        private  Mock<HttpContext> mockHttpContext;
        private  MockHttpSession mockSession;

        private List<Diagnose> fakeDiagnoser;
        private List<DiagnoseGruppe> fakeDiagnoseGrupper;
        private List<Symptom> fakeSymptomer;
        private List<SymptomBilde> fakeSymptomBilder;
        private List<SymptomGruppe> fakeSymptomGrupper;
        private List<SymptomSymptomBilde> fakeSymptomSymptomBilder;


        [TestInitialize]
        public void SetupContext()
        {


            this.fakeDiagnoseGrupper = new List<DiagnoseGruppe>
            {
                new DiagnoseGruppe
                {
                    diagnoseGruppeId=1,
                    beskrivelse="Hjerte problem",
                    navn="hjerte",
                    dypForklaring = "123"
                },
                new DiagnoseGruppe
                {
                    diagnoseGruppeId=2,
                    beskrivelse="Lunge problem",
                    navn="lunge",
                    dypForklaring = "123"
                },
                new DiagnoseGruppe
                {
                    diagnoseGruppeId=3,
                    beskrivelse="mage problem",
                    navn="mage",
                    dypForklaring = "123"
                },
                new DiagnoseGruppe
                {
                    diagnoseGruppeId=4,
                    beskrivelse="hud problem",
                    navn="hud",
                    dypForklaring = "123"
                }
            };


            this.fakeSymptomGrupper = new List<SymptomGruppe>
            {
                new SymptomGruppe
                {
                    symptomGruppeId=1,
                    beskrivelse="Hjerte problem",
                    navn="hjerte",
                    dypForklaring = "123"
                },
                new SymptomGruppe
                {
                    symptomGruppeId=2,
                    beskrivelse="Lunge problem",
                    navn="lunge",
                    dypForklaring = "123"
                },
                new SymptomGruppe
                {
                    symptomGruppeId=3,
                    beskrivelse="mage problem",
                    navn="mage",
                    dypForklaring = "123"
                }
                ,
                new SymptomGruppe
                {
                    symptomGruppeId=4,
                    beskrivelse="andre problem",
                    navn="annet",
                    dypForklaring = "123"
                }
            };



            this.fakeDiagnoser = new List<Diagnose>()
            {

                new Diagnose
                {
                    diagnoseId = 1,
                    beskrivelse = "vondt i venstre-del av hjerte",
                    diagnoseGruppeId = 1,
                    navn = "venstre-del hjerte sykdommen",
                    dypForklaring = "123"
                },
                      new Diagnose
                {
                  diagnoseId = 2,
                    beskrivelse = "vondt i høyre-del av hjerte",
                    diagnoseGruppeId = 1,
                    navn = "høyre-del hjerte sykdommen",
                    dypForklaring = "123"
                },
                            new Diagnose
                {
                    diagnoseId = 3,
                    beskrivelse = "vondt i venstre lunge",
                    diagnoseGruppeId = 2,
                    navn = "venstre lunge sykdom",
                    dypForklaring = "123"
                },      new Diagnose
                {
                    diagnoseId = 4,
                    beskrivelse = "vondt i høyre lunge",
                    diagnoseGruppeId = 2,
                    navn = "høyre lunge sykdom",
                    dypForklaring = "123"
                },      new Diagnose
                {
                    diagnoseId = 5,
                    beskrivelse = "vondt i tarm",
                    diagnoseGruppeId = 3,
                    navn = "tarm sykdommen",
                    dypForklaring = "123"
                },      new Diagnose
                {
                    diagnoseId = 6,
                    beskrivelse = "vondt i makesekk",
                    diagnoseGruppeId = 3,
                    navn = "magesekk sykdommen",
                    dypForklaring = "123"
                }
            };

            this.fakeSymptomer = new List<Symptom>()
            {
                new Symptom
                {
                    beskrivelse = "problemet med syre",
                    navn = "syre problem i magesekk",
                    symptomId = 1,

                    symptomGruppeId =3,
                    dypForklaring = "123"
                },
                   new Symptom
                {
                    beskrivelse = "vondt i lunge",
                    navn = "vondt i lunge",
                    symptomId = 2,

                    symptomGruppeId= 2,
                    dypForklaring = "123"
                },   new Symptom
                {
                    beskrivelse = "vondt i mage",
                    navn = "vondt i mage",
                    symptomId = 3,

                    symptomGruppeId =3,
                    dypForklaring = "123"
                },   new Symptom
                {
                    beskrivelse = "vondt i hjerte",
                    navn = "vondt i hjerte",
                    symptomId = 4,
                   symptomGruppeId=1,
                   dypForklaring = "123"
                },   new Symptom
                {
                    beskrivelse = "har hodepine",
                    navn = "hodepine",
                    symptomId = 5,

                   symptomGruppeId=4,
                   dypForklaring = "123"
                },   new Symptom
                {
                    beskrivelse = "er kvalm",
                    navn = "opplever kvalme",
                    symptomId = 6,
                   symptomGruppeId=4,
                   dypForklaring = "123"
                }
            };


            this.fakeSymptomBilder = new List<SymptomBilde>
            {
                new SymptomBilde
                {
                    diagnoseId = 1,
                    symptomBildeId = 1,
                    beskrivelse = "herte problem",
                    navn = "hjerte vansker",
                    dypForklaring = "123"
                },
                   new SymptomBilde
                {
                    diagnoseId = 4,
                    symptomBildeId = 2,
                    beskrivelse = "lunge problem",
                    navn = "lunge vansker",
                    dypForklaring = "123"
                },
                      new SymptomBilde
                {
                    diagnoseId = 1,
                    symptomBildeId = 3,
                    beskrivelse = "herte har fått hull",
                    navn = "hjerte vansker",
                    dypForklaring = "123"
                }, new SymptomBilde
                {
                    diagnoseId = 2,
                    symptomBildeId = 4,
                    beskrivelse = "lunge punktert",
                    navn = "lunge punktert",
                    dypForklaring = "123"
                },
                   new SymptomBilde
                {
                    diagnoseId = 3,
                    symptomBildeId = 5,
                    beskrivelse = "lunge problem",
                    navn = "lunge vansker",
                    dypForklaring = "123"
                },
                      new SymptomBilde
                {
                    diagnoseId = 5,
                    symptomBildeId = 6,
                    beskrivelse = "tarm har fått hull",
                    navn = "tarm vansker",
                    dypForklaring = "123"
                }, new SymptomBilde
                {
                    diagnoseId = 5,
                    symptomBildeId = 7,
                    beskrivelse = "tarm har tat fyr",
                    navn = "tarm brann",
                    dypForklaring = "123"
                }


            };

            this.fakeSymptomSymptomBilder = new List<SymptomSymptomBilde>
            {
                new SymptomSymptomBilde
                {
                    symptomBildeId = 7,
                    varighet = 2,
                    symptomId = 1
                },
                new SymptomSymptomBilde
                {
                    symptomBildeId = 6,
                    varighet = 1,
                    symptomId = 3
                },
                new SymptomSymptomBilde
                {
                    symptomBildeId = 3,
                    varighet = 3,
                    symptomId = 4
                },
                new SymptomSymptomBilde
                {
                    symptomBildeId = 5,
                    varighet = 1,
                    symptomId = 2
                },
                new SymptomSymptomBilde
                {
                    symptomBildeId = 1,
                    varighet = 2,
                    symptomId = 4
                },
                new SymptomSymptomBilde
                {
                    symptomBildeId = 6,
                    varighet = 4,
                    symptomId = 1
                }
            };
        }


        [TestMethod]
        public void deleteDiagnoseGirOK()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();
            _diagnoseRepository.Setup(k => k.deleteDiagnose(It.IsAny<int>()));

            mockSession[_loggetInn] = _loggetInn;
            mockSession.SetString(_loggetInn, _loggetInn);
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);
            DiagnoseController.ControllerContext.HttpContext = mockHttpContext.Object;

 
        // Act
        var resultat =  DiagnoseController.forgetDiagnose(It.IsAny<int>()).Result as OkObjectResult;
            
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual("ok", resultat.Value);

        }

        [TestMethod]
        public void updateGirOK()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();
            _diagnoseRepository.Setup(k => k.update(It.IsAny<Diagnose>()));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);
            DiagnoseController.ControllerContext.HttpContext = mockHttpContext.Object;
            Diagnose diagnose = fakeDiagnoser[0];
            DiagnoseChangeDTO diagnosen = new DiagnoseChangeDTO { beskrivelse = diagnose.beskrivelse, diagnoseId = diagnose.diagnoseId, dypForklaring = diagnose.dypForklaring };
            // Act
            var resultat = DiagnoseController.update(diagnosen).Result as OkObjectResult;

            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual("ok", resultat.Value);

        }
        [TestMethod]
        public void hentDiagnoseGittDiagnoseIdReturnererDiagnose()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            Diagnose diagnose = fakeDiagnoser[0];
            DiagnoseDetailModel diagnoseDetailModel = new DiagnoseDetailModel { beskrivelse = diagnose.beskrivelse, diagnoseId = diagnose.diagnoseId, dypForklaring = diagnose.dypForklaring };

            _diagnoseRepository.Setup(k => k.hentDiagnoseGittDiagnoseId(diagnose.diagnoseId)).Returns(Task.FromResult(diagnoseDetailModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.hentDiagnoseGittDiagnoseId(diagnose.diagnoseId).Result as OkObjectResult;

            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual<DiagnoseDetailModel>(diagnoseDetailModel, (DiagnoseDetailModel)resultat.Value);
        }

        [TestMethod]
        public void hentSymptomGittSymptomIddReturnererSymptom()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            Symptom symptom = fakeSymptomer[0];
            SymptomDetailModel symptomDetailModel = new SymptomDetailModel { beskrivelse = symptom.beskrivelse, dypForklaring = symptom.dypForklaring, symptomId = symptom.symptomId, symptomGruppeId = symptom.symptomGruppeId, navn = symptom.navn };

            _symptomRepository.Setup(k => k.hentSymptomGittSymptomId(symptom.symptomId)).Returns(Task.FromResult(symptomDetailModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.hentSymptomGittSymptomId(symptom.symptomId).Result as OkObjectResult;

            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual<SymptomDetailModel>(symptomDetailModel, (SymptomDetailModel)resultat.Value);
        }
        [TestMethod]
        public void hentSymptomGruppeGittSymptomGruppeIdReturnerSymptomGruppe()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            SymptomGruppe symptomGruppe = fakeSymptomGrupper[0];
            SymptomGruppeDetailModel symptomGruppeDetailModel = new SymptomGruppeDetailModel { beskrivelse = symptomGruppe.beskrivelse, symptomGruppeId = symptomGruppe.symptomGruppeId, dypForklaring = symptomGruppe.dypForklaring, navn = symptomGruppe.navn };
            _symptomGruppeRepository.Setup(k => k.hentSymptomGruppeGittSymptomGruppeId(symptomGruppe.symptomGruppeId)).Returns(Task.FromResult(symptomGruppeDetailModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.hentSymptomGruppeGittSymptomGruppeId(symptomGruppeDetailModel.symptomGruppeId).Result as OkObjectResult;

            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual<SymptomGruppeDetailModel>(symptomGruppeDetailModel, (SymptomGruppeDetailModel)resultat.Value);
        }

        [TestMethod]
        public void nyDiagnoseGirOk()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            Diagnose diagnose = fakeDiagnoser[0];
            List<int> symptomer = new List<int>(new int[] { 1, 2, 3 });
            List<int> varigheter = new List<int>(new int[] { 2,5,5 });

            DiagnoseCreateDTO diagnoseCreateDTO = new DiagnoseCreateDTO { dypForklaring = diagnose.dypForklaring, beskrivelse = diagnose.beskrivelse, navn = diagnose.navn, symptomer = symptomer, varigheter = varigheter };

            _diagnoseRepository.Setup(k => k.Add(diagnose));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);
            DiagnoseController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = DiagnoseController.nyDiagnose(diagnoseCreateDTO).Result as OkObjectResult;

            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual("ok", resultat.Value);
        }

        [TestMethod]
        public void nyDiagnoseUliktAntallSymptomerOgVarigheterGirBadRequest()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            Diagnose diagnose = fakeDiagnoser[0];
            List<int> symptomer = new List<int>(new int[] { 1, 2, 3 });
            List<int> varigheter = new List<int>(new int[] { 2, 5 });

            DiagnoseCreateDTO diagnoseCreateDTO = new DiagnoseCreateDTO { dypForklaring = diagnose.dypForklaring, beskrivelse = diagnose.beskrivelse, navn = diagnose.navn, symptomer = symptomer, varigheter = varigheter };

            _diagnoseRepository.Setup(k => k.Add(diagnose));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);
            DiagnoseController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = DiagnoseController.nyDiagnose(diagnoseCreateDTO).Result as BadRequestObjectResult;

            // Assert 
            Assert.AreEqual((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.AreEqual("diagnose got bad server input", resultat.Value);
        }


        [TestMethod]
        public void getDiagnoserGittSymptomerReturnererDiagnoser()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List <Diagnose> diagnoser = new List<Diagnose>(new Diagnose[] { fakeDiagnoser[0], fakeDiagnoser[1], fakeDiagnoser[2] });
            List<DiagnoseListModel> diagnoserListModel = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse, diagnoseId = x.diagnoseId, navn = x.navn });

            List<SymptomBilde> symptomBilder = new List<SymptomBilde>(new SymptomBilde[] { fakeSymptomBilder[0], fakeSymptomBilder[1], fakeSymptomBilder[2] });
            List<DiagnoseListModel> diagnoseListModels = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse, diagnoseId = x.diagnoseId, navn = x.navn });
            SymptomDTO symptomDTO1 = new SymptomDTO { id = 1, varighet = 2 };
            SymptomDTO symptomDTO2 = new SymptomDTO { id = 2, varighet = 5 };
            SymptomDTO symptomDTO3 = new SymptomDTO { id = 3, varighet = 5 };

            List<SymptomDTO> symptomDTOs = new List<SymptomDTO>(new SymptomDTO[] { symptomDTO1, symptomDTO2, symptomDTO3 });
            _symptomBildeRepository.Setup((x) => x.hentSymptomBilder(symptomDTOs)).Returns(Task.FromResult(symptomBilder));
            _diagnoseRepository.Setup((x)=>x.hentDiagnoser(symptomBilder)).Returns(Task.FromResult(diagnoserListModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getDiagnoserGittSymptomer(symptomDTOs).Result as OkObjectResult;
            List<DiagnoseListModel> diagnoseListModelReturn = (List<DiagnoseListModel>)resultat.Value;
            Assert.AreEqual<int>(diagnoserListModel.Count, diagnoseListModelReturn.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for(int i = 0; i < diagnoseListModels.Count; i++)
            {
                Assert.AreEqual<DiagnoseListModel>(diagnoserListModel[i], diagnoseListModelReturn[i]);
            }
  

        }
        [TestMethod]
        public void getDiagnoserGittSymptomerMedTomListeReturnererTomListe()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<Diagnose> diagnoser = new List<Diagnose>(new Diagnose[] { fakeDiagnoser[0], fakeDiagnoser[1], fakeDiagnoser[2] });
            List<DiagnoseListModel> diagnoserListModel = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse, diagnoseId = x.diagnoseId, navn = x.navn });

            List<SymptomBilde> symptomBilder = new List<SymptomBilde>(new SymptomBilde[] { fakeSymptomBilder[0], fakeSymptomBilder[1], fakeSymptomBilder[2] });
            List<DiagnoseListModel> diagnoseListModels = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse, diagnoseId = x.diagnoseId, navn = x.navn });
  

            List<SymptomDTO> symptomDTOs = new List<SymptomDTO>();
            _symptomBildeRepository.Setup((x) => x.hentSymptomBilder(symptomDTOs)).Returns(Task.FromResult(symptomBilder));
            _diagnoseRepository.Setup((x) => x.hentDiagnoser(symptomBilder)).Returns(Task.FromResult(diagnoserListModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getDiagnoserGittSymptomer(symptomDTOs).Result as OkObjectResult;
            List<DiagnoseListModel> diagnoseListModelReturn = (List<DiagnoseListModel>)resultat.Value;
    
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            Assert.AreEqual<int>(0, diagnoseListModelReturn.Count);


        }

        [TestMethod]
        public void getDiagnoserGittSymptomerSomGirIngenSymptomBilderReturnererTomListeMedDiagnoser()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<Diagnose> diagnoser = new List<Diagnose>(new Diagnose[] { fakeDiagnoser[0], fakeDiagnoser[1], fakeDiagnoser[2] });
            List<DiagnoseListModel> diagnoserListModel = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse, diagnoseId = x.diagnoseId, navn = x.navn });

            List<SymptomBilde> symptomBilder = new List<SymptomBilde>();
            List<DiagnoseListModel> diagnoseListModels = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse, diagnoseId = x.diagnoseId, navn = x.navn });
            SymptomDTO symptomDTO1 = new SymptomDTO { id = 1, varighet = 2 };
            SymptomDTO symptomDTO2 = new SymptomDTO { id = 2, varighet = 5 };
            SymptomDTO symptomDTO3 = new SymptomDTO { id = 3, varighet = 5 };

            List<SymptomDTO> symptomDTOs = new List<SymptomDTO>(new SymptomDTO[] { symptomDTO1, symptomDTO2, symptomDTO3 });
            _symptomBildeRepository.Setup((x) => x.hentSymptomBilder(symptomDTOs)).Returns(Task.FromResult(symptomBilder));
            _diagnoseRepository.Setup((x) => x.hentDiagnoser(symptomBilder)).Returns(Task.FromResult(diagnoserListModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getDiagnoserGittSymptomer(symptomDTOs).Result as OkObjectResult;
            List<DiagnoseListModel> diagnoseListModelReturn = (List<DiagnoseListModel>)resultat.Value;
   
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.AreEqual<int>(0, diagnoseListModelReturn.Count);



        }

        [TestMethod]
        public void getSymptomerGittGruppeIdGirSymptomListe()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<Symptom> symptomer = new List<Symptom>(new Symptom[] { fakeSymptomer[0], fakeSymptomer[1], fakeSymptomer[2] });
            List<SymptomListModel> symptomListModels = symptomer.ConvertAll((x) => new SymptomListModel { beskrivelse = x.beskrivelse, navn = x.navn, symptomGruppeId = x.symptomGruppeId, symptomId = x.symptomId });

            _symptomRepository.Setup(k=>k.hentSymptomer(fakeSymptomer[0].symptomGruppeId)).Returns(Task.FromResult(symptomListModels));
   
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getSymptomerGittGruppeId(fakeSymptomer[0].symptomGruppeId).Result as OkObjectResult;
            List<SymptomListModel> symptomListModelReturn = (List<SymptomListModel>)resultat.Value;

            // Assert 
            Assert.AreEqual<int>(symptomListModelReturn.Count, symptomListModels.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for (int i = 0; i < symptomListModels.Count; i++)
            {
                Assert.AreEqual<SymptomListModel>(symptomListModels[i], symptomListModelReturn[i]);
            }

        }
        [TestMethod]
        public void getDiagnoseGrupperGirDiagnoseGrupper()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<DiagnoseGruppe> diagnoseGrupper = new List<DiagnoseGruppe>(new DiagnoseGruppe[] { fakeDiagnoseGrupper[0], fakeDiagnoseGrupper[1], fakeDiagnoseGrupper[2] });
            List<DiagnoseGruppeListModel> diagnoserGruppeListModels = diagnoseGrupper.ConvertAll((x) => new DiagnoseGruppeListModel { beskrivelse = x.beskrivelse, navn = x.navn,diagnoseGruppeId=x.diagnoseGruppeId });

            _diagnoseGruppeRepository.Setup(k => k.hentDiagnoseGrupper()).Returns(Task.FromResult(diagnoserGruppeListModels));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getDiagnoseGrupper().Result as OkObjectResult;
            List<DiagnoseGruppeListModel> diagnoseGruppeListModelReturn = (List<DiagnoseGruppeListModel>)resultat.Value;

            // Assert 
            Assert.AreEqual<int>(diagnoseGruppeListModelReturn.Count, diagnoserGruppeListModels.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for (int i = 0; i < diagnoserGruppeListModels.Count; i++)
            {
                Assert.AreEqual<DiagnoseGruppeListModel>(diagnoserGruppeListModels[i], diagnoseGruppeListModelReturn[i]);
            }

        }
        [TestMethod]
        public void getDiagnoserGirDiagnoser()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<Diagnose> diagnoser = new List<Diagnose>(new Diagnose[] { fakeDiagnoser[0], fakeDiagnoser[1], fakeDiagnoser[2] });
            List<DiagnoseListModel> diagnoserListModels = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse = x.beskrivelse,diagnoseId=x.diagnoseId,navn=x.navn});

            _diagnoseRepository.Setup(k => k.hentDiagnoser()).Returns(Task.FromResult(diagnoserListModels));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getDiagnoser().Result as OkObjectResult;
            List<DiagnoseListModel> diagnoseListModelReturn = (List<DiagnoseListModel>)resultat.Value;

            // Assert 
            Assert.AreEqual<int>(diagnoserListModels.Count, diagnoseListModelReturn.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for (int i = 0; i < diagnoserListModels.Count; i++)
            {
                Assert.AreEqual<DiagnoseListModel>(diagnoserListModels[i], diagnoseListModelReturn[i]);
            }

        }

        [TestMethod]
        public void getSymptomerGirSymptomer()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<Symptom> symptomer = new List<Symptom>(new Symptom[] { fakeSymptomer[0], fakeSymptomer[1], fakeSymptomer[2] });
            List<SymptomListModel> symptomListModels = symptomer.ConvertAll((x) => new SymptomListModel {beskrivelse=x.beskrivelse,navn=x.navn,symptomGruppeId=x.symptomGruppeId,symptomId=x.symptomId });

            _symptomRepository.Setup(k => k.hentSymptomer()).Returns(Task.FromResult(symptomListModels));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getSymptomer().Result as OkObjectResult;
            List<SymptomListModel> symptomListModelReturn = (List<SymptomListModel>)resultat.Value;

            // Assert 
            Assert.AreEqual<int>(symptomListModels.Count, symptomListModelReturn.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for (int i = 0; i < symptomListModels.Count; i++)
            {
                Assert.AreEqual<SymptomListModel>(symptomListModels[i], symptomListModelReturn[i]);
            }

        }


        [TestMethod]
        public void getDiagnoserGittGruppeIdGirDiagnoser()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<Diagnose> diagnoser = new List<Diagnose>(new Diagnose[] { fakeDiagnoser[0], fakeDiagnoser[1], fakeDiagnoser[2] });
            List<DiagnoseListModel> diagnoseListModel = diagnoser.ConvertAll((x) => new DiagnoseListModel { beskrivelse=x.beskrivelse,navn=x.navn,diagnoseId=x.diagnoseId});

            _diagnoseRepository.Setup(k => k.hentDiagnoser(fakeDiagnoser[0].diagnoseGruppeId)).Returns(Task.FromResult(diagnoseListModel));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getDiagnoserGittGruppeId(fakeDiagnoser[0].diagnoseGruppeId).Result as OkObjectResult;
            List<DiagnoseListModel> symptomListModelReturn = (List<DiagnoseListModel>)resultat.Value;

            // Assert 
            Assert.AreEqual<int>(symptomListModelReturn.Count, diagnoseListModel.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for (int i = 0; i < diagnoseListModel.Count; i++)
            {
                Assert.AreEqual<DiagnoseListModel>(diagnoseListModel[i], symptomListModelReturn[i]);
            }

        }

        [TestMethod]
        public void getSymptomGrupperGirSymptomGrupper()
        {
            _diagnoseGruppeRepository = new Mock<IDiagnoseGruppeRepository>();
            _diagnoseRepository = new Mock<IDiagnoseRepository>();
            _symptomGruppeRepository = new Mock<ISymptomGruppeRepository>();
            _symptomBildeRepository = new Mock<ISymptomBildeRepository>();
            _symptomRepository = new Mock<ISymptomRepository>();
            _mockLog = new Mock<ILogger<DiagnoseController>>();
            mockHttpContext = new Mock<HttpContext>();
            mockSession = new MockHttpSession();

            List<SymptomGruppe> symptomGrupper = new List<SymptomGruppe>(new SymptomGruppe[] { fakeSymptomGrupper[0], fakeSymptomGrupper[1], fakeSymptomGrupper[2] });
            List<SymptomGruppeListModel> symptomGruppeListModels = symptomGrupper.ConvertAll((x) => new SymptomGruppeListModel { beskrivelse = x.beskrivelse, navn = x.navn, symptomGruppeId = x.symptomGruppeId });

            _symptomGruppeRepository.Setup(k => k.hentSymptomGrupper()).Returns(Task.FromResult(symptomGruppeListModels));

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            var DiagnoseController = new DiagnoseController(_diagnoseRepository.Object, _diagnoseGruppeRepository.Object, _symptomBildeRepository.Object, _symptomGruppeRepository.Object, _symptomRepository.Object, _mockLog.Object);

            // Act
            var resultat = DiagnoseController.getSymptomGrupper().Result as OkObjectResult;
            List<SymptomGruppeListModel> symptomGruppeListModelsReturn = (List<SymptomGruppeListModel>)resultat.Value;

            // Assert 
            Assert.AreEqual<int>(symptomGruppeListModels.Count, symptomGruppeListModelsReturn.Count);
            // Assert 
            Assert.AreEqual((int)HttpStatusCode.OK, resultat.StatusCode);

            for (int i = 0; i < symptomGruppeListModels.Count; i++)
            {
                Assert.AreEqual<SymptomGruppeListModel>(symptomGruppeListModels[i], symptomGruppeListModelsReturn[i]);
            }

        }
        

    }
    
}