import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { route, stringParser } from 'typesafe-routes';
import AuthLayout from './layouts/AuthLayout';
import BaseLayout from './layouts/BaseLayout';
import OrganisationLayout from './layouts/OrganisationLayout';
import ServiceLayout from './layouts/ServiceLayout';
import SuperAdminLayout from './layouts/SuperAdminLayout';
import AdminOrganisationsPage from './pages/admin/AdminOrganisationsPage';
import ChooseOrganisationPage from './pages/ChooseOrganisationPage';
import NotFoundPage from './pages/NotFoundPage';
import CreateServiceTemplatePage from './pages/organisation/CreateServiceTemplatePage';
import OrganisationRolesPage from './pages/organisation/OrganisationRolesPage';
import OrganisationUsersPage from './pages/organisation/OrganisationUsersPage';
import ServiceTemplatesPage from './pages/organisation/ServiceTemplatesPage';
import LocalesPage from './pages/service/LocalesPage';
import ReleasesPage from './pages/service/ReleasesPage';
import ServicesPage from './pages/service/ServicesPage';
import CreateReleasePage from './pages/service/CreateReleasePage';

// --- ADMIN ---
export const adminOrganisationsRoute = route('organisations', {}, {});
export const adminRoute = route(
	'admin',
	{},
	{
		organisations: adminOrganisationsRoute,
	}
);

// --- Organisation ---
export const servicesRoute = route('services', {}, {});
export const createServiceTemplateRoute = route('create', {}, {});
export const serviceTemplatesRoute = route(
	'service-templates',
	{},
	{ create: createServiceTemplateRoute }
);
export const organisationUsersRoute = route('users', {}, {});
export const organisationRolesRoute = route('roles', {}, {});
export const organisationRoute = route(
	'organisation/:name',
	{ name: stringParser },
	{
		users: organisationUsersRoute,
		roles: organisationRolesRoute,
		services: servicesRoute,
		serviceTemplates: serviceTemplatesRoute,
	}
);

// --- SERVICE ---
export const createReleasesRoute = route('create', {}, {});
export const releasesRoute = route(
	'releases',
	{},
	{ create: createReleasesRoute }
);
export const localesRoute = route('locales', {}, {});
export const serviceRoute = route(
	'service/:name',
	{ name: stringParser },
	{ releases: releasesRoute, locales: localesRoute }
);

export const homeRoute = route(
	'/',
	{},
	{
		admin: adminRoute,
		organisation: organisationRoute,
		service: serviceRoute,
	}
);

const Router: React.FC = () => {
	return (
		<Routes>
			<Route element={<AuthLayout />}>
				<Route element={<BaseLayout />}>
					<Route index element={<ChooseOrganisationPage />} />
				</Route>

				<Route
					path={organisationRoute.template}
					element={<OrganisationLayout />}
				>
					<Route
						index
						element={<Navigate to={servicesRoute({}).$} />}
					/>
					<Route
						path={organisationUsersRoute.template}
						element={<OrganisationUsersPage />}
					/>
					<Route
						path={organisationRolesRoute.template}
						element={<OrganisationRolesPage />}
					/>
					<Route path={serviceTemplatesRoute.template}>
						<Route index element={<ServiceTemplatesPage />} />
						<Route
							path={createServiceTemplateRoute.template}
							element={<CreateServiceTemplatePage />}
						/>
					</Route>
					<Route
						path={servicesRoute.template}
						element={<ServicesPage />}
					/>
				</Route>

				<Route path={serviceRoute.template} element={<ServiceLayout />}>
					<Route
						index
						element={<Navigate to={releasesRoute({}).$} />}
					/>

					<Route path={releasesRoute.template}>
						<Route index element={<ReleasesPage />} />

						<Route
							path={createReleasesRoute.template}
							element={<CreateReleasePage />}
						/>
					</Route>

					<Route
						path={localesRoute.template}
						element={<LocalesPage />}
					/>
				</Route>

				<Route
					path={adminRoute.template}
					element={<SuperAdminLayout />}
				>
					<Route
						index
						element={
							<Navigate
								replace
								to={adminOrganisationsRoute({}).$}
							/>
						}
					/>
					<Route
						path={adminOrganisationsRoute.template}
						element={<AdminOrganisationsPage />}
					/>
				</Route>
			</Route>
			<Route path='*' element={<NotFoundPage />} />
		</Routes>
	);
};

export default Router;
