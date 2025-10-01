-- =========================================================
-- DOCTORS (25 rows, different specialties, YOE spread)
-- =========================================================
INSERT INTO barwon_health.Doctor (Name, Email, Phone, Specialty, YOE) VALUES
('Dr. Alice Brown', 'alice.brown@barwon.org', '0412-111-001', 'Cardiology', 12),
('Dr. Michael Smith', 'michael.smith@barwon.org', '0412-111-002', 'Neurology', 8),
('Dr. Sarah Lee', 'sarah.lee@barwon.org', '0412-111-003', 'Pediatrics', 5),
('Dr. David Johnson', 'david.johnson@barwon.org', '0412-111-004', 'Oncology', 15),
('Dr. Emma Davis', 'emma.davis@barwon.org', '0412-111-005', 'Dermatology', 7),
('Dr. George Wilson', 'george.wilson@barwon.org', '0412-111-006', 'Surgery', 20),
('Dr. Helen Clark', 'helen.clark@barwon.org', '0412-111-007', 'Cardiology', 4),
('Dr. Ian Roberts', 'ian.roberts@barwon.org', '0412-111-008', 'Oncology', 9),
('Dr. Julia Hall', 'julia.hall@barwon.org', '0412-111-009', 'Pediatrics', 2),
('Dr. Kevin Turner', 'kevin.turner@barwon.org', '0412-111-010', 'Dermatology', 6),
('Dr. Linda Scott', 'linda.scott@barwon.org', '0412-111-011', 'Cardiology', 11),
('Dr. Mark Evans', 'mark.evans@barwon.org', '0412-111-012', 'Neurology', 3),
('Dr. Nancy Young', 'nancy.young@barwon.org', '0412-111-013', 'Oncology', 17),
('Dr. Oscar King', 'oscar.king@barwon.org', '0412-111-014', 'Dermatology', 1),
('Dr. Paula Wright', 'paula.wright@barwon.org', '0412-111-015', 'Surgery', 10),
('Dr. Quentin Adams', 'quentin.adams@barwon.org', '0412-111-016', 'Cardiology', 9),
('Dr. Rachel Baker', 'rachel.baker@barwon.org', '0412-111-017', 'Neurology', 13),
('Dr. Steve Carter', 'steve.carter@barwon.org', '0412-111-018', 'Dermatology', 5),
('Dr. Tina Foster', 'tina.foster@barwon.org', '0412-111-019', 'Oncology', 8),
('Dr. Umar Green', 'umar.green@barwon.org', '0412-111-020', 'Cardiology', 6),
('Dr. Victor Hughes', 'victor.hughes@barwon.org', '0412-111-021', 'Pediatrics', 12),
('Dr. Wendy Irving', 'wendy.irving@barwon.org', '0412-111-022', 'Neurology', 14),
('Dr. Xavier James', 'xavier.james@barwon.org', '0412-111-023', 'Oncology', 5),
('Dr. Yolanda Knight', 'yolanda.knight@barwon.org', '0412-111-024', 'Dermatology', 16),
('Dr. Zack Lewis', 'zack.lewis@barwon.org', '0412-111-025', 'Surgery', 7);

-- =========================================================
-- PATIENTS (25 rows, age spread, some NULL emails, some 25)
-- =========================================================
INSERT INTO barwon_health.Patient (Name, Address, Age, Email, Phone, Medicare_Card_Num, Doctor_ID) VALUES
('John Doe', '12 King St, Melbourne VIC', 45, 'john.doe@mail.com', '0491-111-111', 100001, 1),
('Mary Green', '55 High Rd, Sydney NSW', 25, NULL, '0491-111-112', 100002, 2),
('Peter White', '88 Main St, Geelong VIC', 27, 'peter.white@mail.com', '0491-111-113', 100003, 2),
('Emma Wilson', '101 Bay Rd, Melbourne VIC', 6, 'emma.wilson@mail.com', '0491-111-114', 100004, 3),
('George Miller', '7 Ocean Dr, Adelaide SA', 67, 'george.miller@mail.com', '0491-111-115', 100005, 4),
('Sophia Brown', '9 Hill St, Sydney NSW', 25, 'sophia.brown@mail.com', '0491-111-116', 100006, 5),
('Liam Jones', '22 Lake Rd, Geelong VIC', 18, NULL, '0491-111-117', 100007, 6),
('Olivia Taylor', '30 River St, Melbourne VIC', 30, 'olivia.taylor@mail.com', '0491-111-118', 100008, 7),
('Noah Harris', '44 Park Rd, Sydney NSW', 50, 'noah.harris@mail.com', '0491-111-119', 100009, 8),
('Ava Martin', '70 Station St, Adelaide SA', 25, 'ava.martin@mail.com', '0491-111-120', 100010, 9),
('James Lee', '81 Main Rd, Melbourne VIC', 40, 'james.lee@mail.com', '0491-111-121', 100011, 10),
('Mia Walker', '99 High St, Geelong VIC', 23, 'mia.walker@mail.com', '0491-111-122', 100012, 11),
('Ethan Hall', '102 West Rd, Sydney NSW', 35, 'ethan.hall@mail.com', '0491-111-123', 100013, 12),
('Amelia Allen', '14 Bay St, Adelaide SA', 29, NULL, '0491-111-124', 100014, 13),
('Lucas Young', '200 City Rd, Geelong VIC', 19, 'lucas.young@mail.com', '0491-111-125', 100015, 14),
('Charlotte King', '300 River Rd, Melbourne VIC', 55, 'charlotte.king@mail.com', '0491-111-126', 100016, 15),
('Benjamin Scott', '17 Ocean Dr, Sydney NSW', 60, 'benjamin.scott@mail.com', '0491-111-127', 100017, 16),
('Harper Adams', '88 Market St, Geelong VIC', 22, 'harper.adams@mail.com', '0491-111-128', 100018, 17),
('Daniel Wright', '45 Lake St, Adelaide SA', 28, 'daniel.wright@mail.com', '0491-111-129', 100019, 18),
('Ella Turner', '13 Main St, Melbourne VIC', 25, NULL, '0491-111-130', 100020, 19),
('Henry Baker', '9 Hill Rd, Sydney NSW', 32, 'henry.baker@mail.com', '0491-111-131', 100021, 20),
('Grace Carter', '7 Queen St, Geelong VIC', 25, 'grace.carter@mail.com', '0491-111-132', 100022, 21),
('Jack Foster', '2 Bay St, Adelaide SA', 42, 'jack.foster@mail.com', '0491-111-133', 100023, 22),
('Lily Hughes', '10 King Rd, Melbourne VIC', 21, 'lily.hughes@mail.com', '0491-111-134', 100024, 23),
('Mason Irving', '4 Park St, Sydney NSW', 37, 'mason.irving@mail.com', '0491-111-135', 100025, 24);

-- =========================================================
-- COMPANIES (5 rows)
-- =========================================================
INSERT INTO barwon_health.Company (Name, Address, Phone) VALUES
('MediPharma Ltd', '200 Health St, Melbourne VIC', '0390-111-001'),
('BioCare Pty', '15 Wellness Ave, Sydney NSW', '0298-111-002'),
('AussieMeds', '300 Victoria Rd, Brisbane QLD', '0733-111-003'),
('HealthCorp', '100 City Ave, Adelaide SA', '0881-111-004'),
('WellLife Pharma', '50 Queen St, Geelong VIC', '0355-111-005');

-- =========================================================
-- DRUGS (10 rows, linked to companies)
-- =========================================================
INSERT INTO barwon_health.Drug (Drug_strength, Trade_name, Company_ID) VALUES
('500mg', 'CardioPlus', 1),
('250mg', 'NeuroMax', 2),
('100mg', 'KidCare', 2),
('20mg', 'OncoRelief', 3),
('50mg', 'PainAway', 1),
('200mg', 'DermaHeal', 4),
('75mg', 'NeuroLite', 2),
('10mg', 'OncoSafe', 3),
('150mg', 'MediCure', 5),
('300mg', 'CardioSafe', 1);

-- =========================================================
-- PRESCRIPTIONS (25 rows, every patient at least 1 prescription)
-- =========================================================
INSERT INTO barwon_health.Doctor_Prescribe_Patient_Drug (Doc_ID, Patient_ID, Drug_ID, Date, Quantity) VALUES
(1, 1, 1, '2024-08-15', 30),
(2, 2, 2, '2024-09-01', 20),
(2, 3, 2, '2024-09-02', 15),
(3, 4, 3, '2024-09-05', 10),
(4, 5, 4, '2024-09-07', 25),
(5, 6, 6, '2024-09-10', 12),
(6, 7, 5, '2024-09-12', 18),
(7, 8, 1, '2024-09-14', 22),
(8, 9, 4, '2024-09-16', 9),
(9, 10, 3, '2024-09-18', 14),
(10, 11, 2, '2024-09-20', 11),
(11, 12, 1, '2024-09-21', 19),
(12, 13, 7, '2024-09-22', 13),
(13, 14, 6, '2024-09-23', 8),
(14, 15, 5, '2024-09-24', 10),
(15, 16, 10, '2024-09-25', 17),
(16, 17, 8, '2024-09-26', 16),
(17, 18, 9, '2024-09-27', 7),
(18, 19, 4, '2024-09-28', 20),
(19, 20, 2, '2024-09-29', 15),
(20, 21, 1, '2024-09-30', 5),
(21, 22, 6, '2024-10-01', 12),
(22, 23, 7, '2024-10-02', 9),
(23, 24, 9, '2024-10-03', 14),
(24, 25, 10, '2024-10-04', 21);
