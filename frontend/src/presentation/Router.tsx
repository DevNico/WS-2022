import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { route, stringParser } from 'typesafe-routes';
import HomeLayout from './layouts/HomeLayout';
import OrganisationLayout from './layouts/OrganisationLayout';
import SuperAdminLayout from './layouts/SuperAdminLayout';
import AdminOrganisationsPage from './pages/AdminOrganisationsPage';
import ChooseOrganisationPage from './pages/ChooseOrganisationPage';
import NotFoundPage from './pages/NotFoundPage';
import OrganisationPage from './pages/OrganisationPage';
import OrganisationRolesPage from './pages/OrganisationRolesPage';
import OrganisationUsersPage from './pages/OrganisationUsersPage';

export const adminOrganisationsRoute = route('organisations', {}, {});
export const adminRoute = route(
	'admin',
	{},
	{ organisations: adminOrganisationsRoute }
);

export const organisationUsersRoute = route(
	'users',
	{ name: stringParser },
	{}
);
export const organisationRolesRoute = route(
	'roles',
	{ name: stringParser },
	{}
);
export const organisationRoute = route(
	'organisation/:name',
	{ name: stringParser },
	{ users: organisationUsersRoute, roles: organisationRolesRoute }
);

export const homeRoute = route(
	'/',
	{},
	{
		admin: adminRoute,
		organisation: organisationRoute,
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
						element={<Navigate to={organisationUsersRoute({}).$} />}
					/>
					<Route
						path={organisationUsersRoute.template}
						element={<OrganisationUsersPage />}
					/>
					<Route
						path={organisationRolesRoute.template}
						element={<OrganisationRolesPage />}
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
