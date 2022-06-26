import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { route, stringParser } from 'typesafe-routes';
import HomeLayout from './layouts/HomeLayout';
import OrganisationLayout from './layouts/OrganisationLayout';
import SuperAdminLayout from './layouts/SuperAdminLayout';
import AdminOrganisationsPage from './pages/AdminOrganisationsPage';
import ChooseOrganisationPage from './pages/ChooseOrganisationPage';
import NotFoundPage from './pages/NotFoundPage';
import OrganisationRolesPage from './pages/OrganisationRolesPage';
import OrganisationUsersPage from './pages/OrganisationUsersPage';
import ReleasesPage from './pages/ReleasesPage';
import ServicesPage from './pages/ServicesPage';

// --- ADMIN ---
export const adminOrganisationsRoute = route('organisations', {}, {});
export const adminRoute = route(
	'admin',
	{},
	{ organisations: adminOrganisationsRoute }
);

// --- Organisation ---
export const servicesRoute = route('services', {}, {});
export const organisationUsersRoute = route('users', {}, {});
export const organisationRolesRoute = route('roles', {}, {});
export const organisationRoute = route(
	'organisation/:name',
	{ name: stringParser },
	{
		users: organisationUsersRoute,
		roles: organisationRolesRoute,
		services: servicesRoute,
	}
);

// --- SERVICE ---
export const releasesRoute = route('releases', {}, {});
export const serviceRoute = route(
	'service/:name',
	{ name: stringParser },
	{ releases: releasesRoute }
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
			<Route element={<HomeLayout />}>
				<Route index element={<ChooseOrganisationPage />} />

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
					<Route
						path={servicesRoute.template}
						element={<ServicesPage />}
					/>
				</Route>

				<Route path={serviceRoute.template}>
					<Route
						index
						element={<Navigate to={releasesRoute({}).$} />}
					/>

					<Route
						path={releasesRoute.template}
						element={<ReleasesPage />}
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
