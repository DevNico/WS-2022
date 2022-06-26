import Grid from '@mui/material/Grid';
import React from 'react';
import { useRouteParams } from 'typesafe-routes';
import { OrganisationRecord, ServiceRecord } from '../../api/models';
import { useOrganisationsGetByRouteName } from '../../api/organisation/organisation';
import { useServiceList } from '../../api/service-endpoints/service-endpoints';
import QueryWrapper from '../components/QueryWrapper';
import CreateServiceCard from '../components/service/CreateServiceCard';
import ServiceCard from '../components/service/ServiceCard';
import { organisationRoute } from '../Router';

const ServicesPage: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisationResult = useOrganisationsGetByRouteName(name);
	const servicesResult = useServiceList(name);

	return (
		<QueryWrapper<OrganisationRecord> result={organisationResult}>
			{(organisation) => (
				<QueryWrapper<ServiceRecord[]> result={servicesResult}>
					{(services) => (
						<Grid container spacing={[2, 4]}>
							{services!.map((service) => (
								<Grid
									item
									key={service.id}
									lg={4}
									md={6}
									sm={12}
									xs={12}
								>
									<ServiceCard
										service={service}
										sx={{
											height: '100%',
										}}
									/>
								</Grid>
							))}
							<Grid item lg={4} md={6} sm={12} xs={12}>
								<CreateServiceCard
									organisation={organisation}
									sx={{
										height: '100%',
									}}
								/>
							</Grid>
						</Grid>
					)}
				</QueryWrapper>
			)}
		</QueryWrapper>
	);
};

export default ServicesPage;
