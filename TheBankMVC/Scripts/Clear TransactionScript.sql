
--Clear Transactions

UPDATE UserAccounts SET ShareSubmitted = 0, FineSubmitted = 0, InterestSubmitted = 0, AmountOnLoan = 0
DELETE FROM EMIHeaders
DELETE FROM Installments
DELETE FROM Transactions


--Reset database

DELETE FROM EMIHeaders
DELETE FROM Installments
DELETE FROM Transactions
DELETE FROM GroupUserMappings
DELETE FROM UserAccounts
DELETE FROM Groups


