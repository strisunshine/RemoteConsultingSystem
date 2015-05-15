insert into AspNetRoles(Id,Name) values(1, 'admin');
insert into AspNetRoles(Id,Name) values(2, 'secretary');
insert into AspNetRoles(Id,Name) values(3, 'client');
select * from AspNetRoles;
select * from AspNetUsers;
delete from AspNetUsers where Id='18f2bdb2-91fc-4486-86a4-8d01f4eb8619';
delete  from AspNetUsers where UserName = 'admin';
insert into AspNetUserRoles(UserId,RoleId) values('2e32f6f4-e174-41f6-8a08-fba09d3675b4',1);
insert into AspNetUserRoles(UserId,RoleId) values('2ca078d8-3f9d-4b09-b975-ac288b1130fc',2);
insert into AspNetUserRoles(UserId,RoleId) values('2933c7cc-4b1d-48be-a425-6b1661283040',3);
insert into AspNetUserRoles(UserId,RoleId) values('e0894302-3d60-43f9-963e-d8a690d5b376',3);
insert into AspNetUserRoles(UserId,RoleId) values('dd7fc443-6306-4f4e-b4bd-3de27c3cd076',3);
select * from AspNetUserRoles;

