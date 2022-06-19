import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomeLayout from './layouts/HomeLayout';
import HomePage from './pages/HomePage';
import NotFoundPage from './pages/NotFoundPage';
import OrganisationsPage from './pages/OrganisationsPage';
import CreateUsersPage from './pages/CreateUsersPage';
import UnauthorizedPage from './pages/UnauthorizedPage';
import CreateRolePage from './pages/CreateRolePage';
import UsersPage from './pages/UsersPage';
import RolesPage from './pages/RolesPage';
import CreateOrganisationPage from './pages/CreateOrganisationPage';

const Router: React.FC = () => {
	return (
		<Routes>
			<Route path='/' element={<HomeLayout />}>
				<Route index element={<HomePage />} />
				<Route path='/organisations' element={<OrganisationsPage />} />
				<Route
					path='/organisations/create'
					element={<CreateOrganisationPage />}
				/>
				<Route path='/users' element={<UsersPage />} />
				<Route path='/users/create' element={<CreateUsersPage />} />
				<Route path='/roles' element={<RolesPage />} />
				<Route path='/roles/create' element={<CreateRolePage />} />
				<Route path='/unauthorized' element={<UnauthorizedPage />} />
			</Route>
			<Route path='*' element={<NotFoundPage />} />
		</Routes>
	);
};

export default Router;
