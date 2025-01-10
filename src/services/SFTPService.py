from typing import List
from interfaces import ISFTPService


class SFTPService(ISFTPService):
    def __init__(self, secret: str):
        self.secret = secret
        self.sftp_client = None
        self.ssh_client = None

    def connect(self):
        """Establishes the SFTP connection."""
        pass

    def close(self):
        """Closes the SFTP connection."""
        pass

    def fetch_files(self, directory: str) -> List:
        """Lists files in the given directory."""
        return [] 