using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using ProyectoIA.negocio;
namespace ProyectoIA.util
{
    public class FaceApi
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("3cb39c7bfec14d77927097de75d07028", "https://westcentralus.api.cognitive.microsoft.com/face/v1.0");
        string personGroupId = "estudiante";
        public FaceApi()
        {
            
            faceServiceClient.CreatePersonGroupAsync(personGroupId, "Estudiantes");

        }
        public async void crearEstudiante(string id)
        {
            CreatePersonResult friend1 = await faceServiceClient.CreatePersonAsync(
                // Id of the PersonGroup that the person belonged to
                personGroupId,
                // Name of the person
                id
            );
            
        }
        public async void agregarFotoEstudiante(string id, Image image)
        {
            CreatePersonResult estudiante = await faceServiceClient.CreatePersonAsync(
                // Id of the PersonGroup that the person belonged to
                personGroupId,
                // Name of the person
                id
            );
            Stream s = ToStream(image, ImageFormat.Jpeg);
            await faceServiceClient.AddPersonFaceAsync(
                personGroupId, estudiante.PersonId, s);

        }


        public static Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
        public async void entrenar()
        {
            await faceServiceClient.TrainPersonGroupAsync(personGroupId);
            TrainingStatus trainingStatus = null;
            while (true)
            {
                trainingStatus = await faceServiceClient.GetPersonGroupTrainingStatusAsync(personGroupId);

                if (trainingStatus.Status != Status.Running)
                {
                    break;
                }

                await Task.Delay(1000);
            }
            MessageBox.Show("entrenado");
        }
        public async Task<string> detectar(Image image)
        {
            Stream s = ToStream(image, ImageFormat.Jpeg);
            var faces = await faceServiceClient.DetectAsync(s);
            var faceIds = faces.Select(face => face.FaceId).ToArray();

            var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
            foreach (var identifyResult in results)
            {
                Debug.WriteLine("Result of face: {0}", identifyResult.FaceId);
                //Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
                if (identifyResult.Candidates.Length == 0)
                {
                    Debug.WriteLine("No one identified");
                    //Console.WriteLine("No one identified");
                    return null;
                }
                else
                {
                    // Get top 1 among all candidates returned
                    var candidateId = identifyResult.Candidates[0].PersonId;
                    var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                    Debug.WriteLine("Identified as {0}", person.Name);
                    return person.Name;
                    
                    //Console.WriteLine("Identified as {0}", person.Name);
                }
            }
            return null;
        }

    }
}
