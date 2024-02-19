import { createApp } from 'vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import './0-Global/style/utilities/bootstrap.css';
import './0-Global/style/utilities/_variables.css';
import './0-Global/style/utilities/fontawesome.css';
import './0-Global/style/base/reset.css';
import './0-Global/style/base/global.css';
import App from './App.vue';

const app = createApp(App);

app.component('font-awesome-icon', FontAwesomeIcon);
app.mount('#app');
