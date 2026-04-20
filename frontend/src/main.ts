import { createApp } from 'vue';
import { createPinia } from 'pinia';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { vFadeUp } from './0-Global/directives/fade-up';
import './0-Global/style/utilities/_variables.css';
import './0-Global/style/utilities/fontawesome.css';
import './0-Global/style/base/reset.css';
import './0-Global/style/base/global.scss';
import './0-Global/style/base/_terminal.scss';
import App from './App.vue';
import router from './0-Global/router';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.component('font-awesome-icon', FontAwesomeIcon);
app.directive('fade-up', vFadeUp);

window.addEventListener('auth:session-expired', () => router.push('/login'));

app.mount('#app');
