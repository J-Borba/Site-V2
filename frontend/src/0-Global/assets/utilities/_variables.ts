interface linkObject {
  title: string;
  shortTitle?: string;
  url: string;
  startYear?: number;
}

export const uerj: linkObject = {
  title: 'Universidade do Estado do Rio de Janeiro',
  shortTitle: 'UERJ',
  url: 'https://www.uerj.br/',
};

export const tiplan: linkObject = {
  title: 'TIPLAN',
  url: 'https://www.tiplan.com.br/',
  startYear: 2023,
};

export const abbr: linkObject = {
  title: 'Associação Brasileira Beneficente de Reabilitação',
  shortTitle: 'ABBR',
  url: 'https://abbr.org.br/',
  startYear: 2022,
};

export const myGithub: linkObject = {
  title: 'Github',
  url: 'https://github.com/J-Borba',
};
