module.exports = {
  env: {
    node: true,
  },
  extends: ['eslint:recommended', 'plugin:vue/vue3-recommended', 'prettier'],
  rules: {
    'semi-spacing': 'warn',
    'arrow-parens': 'warn',
    'comma-dangle': 'always',
  },
};
