-- sp

CREATE OR REPLACE FUNCTION calculate_member_fine(p_member_id INT)
RETURNS DECIMAL
AS
$$
DECLARE
    total_fine DECIMAL;
BEGIN

    SELECT COALESCE(SUM(f."Amount"), 0)
    INTO total_fine
    FROM "Fines" f
    INNER JOIN "BorrowTransactions" bt
        ON f."BorrowId" = bt."BorrowId"
    WHERE bt."MemberId" = p_member_id
    AND f."IsPaid" = false;     -- check for unpaid fines

    RETURN total_fine;

END;
$$
LANGUAGE plpgsql;


-- Initial data


INSERT INTO "Categories" ("Name") VALUES 
('Science Fiction'),
('Technology'),
('History');


INSERT INTO "MembershipTypes" ("Name", "MaxBooks", "MaxDays") VALUES 
('Basic', 2, 7),
('Student', 3, 10),
('Premium', 5, 15);



INSERT INTO "Members" ("Name", "Email", "Phone", "Password", "MembershipTypeId", "IsActive", "MemberRole") VALUES 
('Library Admin', 'admin@library.com', '9876543210', 'admin123', 2, true, 1),
('John Doe', 'john@gmail.com', '1234567890', 'password', 1, true, 0);


INSERT INTO "BookTitles" ("Title", "Author", "PublishedYear", "CategoryId") VALUES 
('Dune', 'Frank Herbert', 1965, 1),
('Clean Code', 'Robert C. Martin', 2008, 2),
('Sapiens', 'Yuval Noah Harari', 2011, 3);



INSERT INTO "BookCopies" ("CopyCode", "BookTitleId", "Status") VALUES 
('DUNE-01', 1, 0),
('DUNE-02', 1, 0),
('CC-01', 2, 0),
('CC-02', 2, 0),
('SAP-01', 3, 0);
