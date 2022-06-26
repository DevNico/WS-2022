import React from 'react';
import { Navigate } from 'react-router-dom';
import { useRouteParams } from 'typesafe-routes';
import { useServiceGetByRouteName } from '../../api/service/service';
import QueryWrapper from '../components/QueryWrapper';
import { serviceRoute } from '../Router';
import BaseLayout from './BaseLayout';
import ServiceDrawer from './components/ServiceDrawer';

const ServiceLayout: React.FC = () => {
	const { name } = useRouteParams(serviceRoute);
	const service = useServiceGetByRouteName(name);

	return (
		<QueryWrapper result={service} error={<Navigate to={'/404'} />}>
			{() => <BaseLayout drawer={<ServiceDrawer />} />}
		</QueryWrapper>
	);
};

export default ServiceLayout;
