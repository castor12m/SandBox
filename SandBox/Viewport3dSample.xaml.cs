using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace SandBox
{
    /// <summary>
    /// Viewport3dSample.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Viewport3dSample : Window
    {
        public Viewport3dSample()
        {
            InitializeComponent();
        }

        private void simpleButtonClick(object sender, RoutedEventArgs e)
        {
            //https://crystalcube.co.kr/52

            // MeshGeometry3D 생성
            MeshGeometry3D triangleMesh = new MeshGeometry3D();

            // 삼각형의 세점 정의
            Point3D point0 = new Point3D(0, 0, 0); 
            Point3D point1 = new Point3D(5, 0, 0); 
            Point3D point2 = new Point3D(0, 0, 5);

            // Mesh에 위 세점 등록
            triangleMesh.Positions.Add(point0); 
            triangleMesh.Positions.Add(point1); 
            triangleMesh.Positions.Add(point2);

            // 삼각형 index 추가
            triangleMesh.TriangleIndices.Add(0); 
            triangleMesh.TriangleIndices.Add(2); 
            triangleMesh.TriangleIndices.Add(1);

            // 법선 벡터 추가
            Vector3D normal = new Vector3D(0, 1, 0); 
            triangleMesh.Normals.Add(normal); 
            triangleMesh.Normals.Add(normal); 
            triangleMesh.Normals.Add(normal);

            // 표면 생성
            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.DarkKhaki)); 
            GeometryModel3D triangleModel = new GeometryModel3D(triangleMesh, material); 
            ModelVisual3D model = new ModelVisual3D(); 
            model.Content = triangleModel; 
            this.mainViewport.Children.Add(model);

        }
    }
}
