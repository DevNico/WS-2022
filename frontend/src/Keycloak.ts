import Keycloak from 'keycloak-js';

const keycloak = new Keycloak({
	clientId: 'webapp-v1',
	realm: 'dev',
	url: 'https://idp.srm.k3s.devnico.cloud',
});

export default keycloak;
