from abc import ABC, abstractmethod

class ISFTPService(ABC):
    @abstractmethod
    def connect(self):
        """Establishes the SFTP connection."""
        pass

    @abstractmethod
    def close(self):
        """Closes the SFTP connection."""
        pass

    @abstractmethod
    def fetch_files(self, directory: str) -> List:
        """Lists files in the given directory."""
        pass