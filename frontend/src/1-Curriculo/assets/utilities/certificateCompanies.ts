import aluraIcon from '../images/alura.jpeg';
import udemyIcon from '../images/udemy.jpeg';
import rocketseatIcon from '../images/rocketseat.jpeg';

interface ICertificate {
  title: string;
  url: string;
}

interface IDegree {
  title?: string;
  url?: string;
  certificates: Array<ICertificate>;
}

interface ICompany {
  name: string;
  icon: string;
  degrees: Array<IDegree>;
}

export const CertificateCompanies: Array<ICompany> = [
  {
    name: 'Alura',
    icon: aluraIcon,
    degrees: [
      {
        title: 'Formação - ASP.Net Core: crie aplicações com C#, .NET, Entity Framework e LINQ',
        url: 'https://cursos.alura.com.br/degree/certificate/2a550f53-9880-4125-a711-a4e44f93143f',
        certificates: [
          {
            title: '.NET 6 e Identity: implementando controle de usuário',
            url: 'https://cursos.alura.com.br/certificate/4c41847c-96c5-45fc-b3d1-bafb1eade887',
          },
          {
            title: '.NET 6: relacionando entidades',
            url: 'https://cursos.alura.com.br/certificate/310b7b5b-eff5-4114-b888-2d938d9b15f0',
          },
          {
            title: '.NET 6: criando uma web API',
            url: 'https://cursos.alura.com.br/certificate/0a73ce25-571d-4d5d-8d42-b84a57123a78?lang=pt_BR',
          },
          {
            title: 'HTTP: entendendo a web por baixo dos panos',
            url: 'https://cursos.alura.com.br/certificate/6251e3fc-d35f-4e7c-9c30-b5d1ab07db59',
          },
        ],
      },
      {
        title: 'Formação - Boas práticas em C#',
        url: 'https://cursos.alura.com.br/degree/certificate/b6128e3d-3567-452a-9d36-6d0fdef3914e',
        certificates: [
          {
            title: 'C#: aplique princípios SOLID',
            url: 'https://cursos.alura.com.br/certificate/963b22bb-d039-447c-993e-d642ba4a136d',
          },
          {
            title: 'C#: aprenda a criar testes limpos com boas práticas',
            url: 'https://cursos.alura.com.br/certificate/c0a9857b-bd14-4a59-ad20-3a6d59cc03d9',
          },
          {
            title: 'C#: aplique boas práticas de refatoração e torne seu código mais legível',
            url: 'https://cursos.alura.com.br/certificate/96d2a707-1f3e-41f7-b12a-cc21995af558',
          },
        ],
      },
    ],
  },
  {
    name: 'Rocketseat',
    icon: rocketseatIcon,
    degrees: [
      {
        // Não apresenta titulo nem URL de Formação pois Rocketseat oferece cursos soltos
        certificates: [
          {
            title: 'NLW Unite - Csharp',
            url: 'https://app.rocketseat.com.br/certificates/ed6219d5-f7ec-4b57-9d39-115722bb49f1',
          },
          {
            title: 'NLW Unite - Reactjs',
            url: 'https://app.rocketseat.com.br/certificates/a44b244e-6411-46db-b954-81437428a032',
          },
          {
            title: 'NLW Unite - React Native',
            url: 'https://app.rocketseat.com.br/certificates/79e5506b-d072-4b93-84de-5efe9756a2fc',
          },
        ],
      },
    ],
  },
  {
    name: 'Udemy',
    icon: udemyIcon,
    degrees: [
      {
        // Não apresenta titulo nem URL de Formação pois Udemy oferece cursos soltos
        certificates: [
          {
            title: 'C# COMPLETO Programação Orientada a Objetos + Projetos',
            url: 'https://www.udemy.com/certificate/UC-855c52b9-7d99-4268-814a-b518bef8c295/',
          },
          {
            title: 'The Complete 2023 Web Development Bootcamp',
            url: 'https://www.udemy.com/certificate/UC-3e28fcf8-faed-4241-9940-002f7c1afb63/',
          },
          {
            title: 'Power BI Completo - Do Básico ao Avançado',
            url: 'https://www.udemy.com/certificate/UC-d2e1559c-5b7a-45b7-9168-e1db9a8aa03d/',
          },
        ],
      },
    ],
  },
];
