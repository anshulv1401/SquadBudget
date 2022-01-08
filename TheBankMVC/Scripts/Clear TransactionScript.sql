SELECT * FROM Bank
SELECT * FROM UserAccount
SELECT * FROM BankUserMappings
SELECT * FROM EMIHeaders
SELECT * FROM Installments
SELECT * FROM Transactions

UPDATE UserAccount SET ShareSubmitted = 0, FineSubmitted = 0, InterestSubmitted = 0, AmountOnLoan = 0
DELETE FROM EMIHeaders
DELETE FROM Installments
DELETE FROM Transactions

UPDATE Installments SET DueDate = DATEADD(MONTH, -1, DueDate) WHERE InstallmentStatus <> 2 
UPDATE Installments SET Fine = 0 WHERE InstallmentStatus <> 2 

--UPDATE Installments SET DueDate = CAST(DueDate AS DATE)

SELECT * FROM Installments WHERE InstallmentStatus <> 2 ORDER BY DueDate

select * from EMIHeaders

