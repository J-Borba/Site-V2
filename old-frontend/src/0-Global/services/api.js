import axios from 'axios';

export const githubApi = axios.create({
  baseURL: 'https://api.github.com/users/J-Borba/',
});

export const myApi = axios.create({
  baseURL: 'https://localhost:7044/',
});
